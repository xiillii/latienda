using System.Web.Http;
using Microsoft.Azure.Mobile.Server.Config;
using System.Threading.Tasks;
using System;
using Microsoft.Azure.NotificationHubs;
using System.Collections.Generic;

namespace latienda.notifications.backend.Controllers
{
    [RoutePrefix("alerts")]
    public class AlertsController : ApiController
    {
        
        [HttpPost]
        [Route("")]
        public async Task<IHttpActionResult> SendNotificationAsync([FromBody] string message)
        {
            // get the settings
            var config = Configuration;
            try
            {
                await SendNotificationInternalAsync(message, null);
            }
            catch (Exception ex)
            {
                config.Services.GetTraceWriter().Error(ex.Message, null, $"Push.SendAsync error");
                return BadRequest(ex.Message);
            }

            return Ok();
        }

        [HttpPost]
        [Route("{installationId}")]
        public async Task<IHttpActionResult> SendNotificationAsync(string installationId, [FromBody] string message)
        {
            // get the settings
            var config = Configuration;
            try
            {
                await SendNotificationInternalAsync(message, installationId);
            }
            catch (Exception ex)
            {
                config.Services.GetTraceWriter().Error(ex.Message, null, $"Push.SendAsync error");
                return BadRequest(ex.Message);
            }

            return Ok();
        }

        private async Task<NotificationOutcome> SendNotificationInternalAsync(string message, string installationId)
        {
            // Get the settings
            var config = Configuration;

#if USE_APP_SETTINGS
            var settings = config.GetMobileAppSettingsProvider().GetMobileAppSettings();

			// Get the Notification Hubs credentials for the Mobile App.
			var notificationHubName = settings.NotificationHubName;
			var notificationHubConnection = settings.Connections[MobileAppSettingsKeys.NotificationHubConnectionString].ConnectionString;
#else
            // The name of the Notification Hub from the overview page.
            var notificationHubName = "LatienditaNHXamarin";
            // Use "DefaultFullSharedAccessSignature" from the portal's Access Policies.
            var notificationHubConnection = "Endpoint=sb://latienditanhxamarin.servicebus.windows.net/;SharedAccessKeyName=DefaultFullSharedAccessSignature;SharedAccessKey=ROlK3ayqTz+bTsM6HSfSuhJdysyW9enJA3Ykl5dkPiA=";
#endif

            // Create a new Notification Hub client.
            var hub = NotificationHubClient.CreateClientFromConnectionString(
                notificationHubConnection,
                notificationHubName,
                // Don't use this in RELEASE builds. The number of devices is limited.
                // If TRUE, the send method will return the devices a message was
                // delivered to.
                enableTestSend: true);

            // Sending the message so that all template registrations that contain "messageParam"
            // will receive the notifications. This includes APNS, GCM, WNS, and MPNS template registrations.
            var templateParams = new Dictionary<string, string>
            {
                ["messageParam"] = message
            };

            // Send the push notification and log the results.

            NotificationOutcome result;
            if (string.IsNullOrWhiteSpace(installationId))
            {
                result = await hub.SendTemplateNotificationAsync(templateParams).ConfigureAwait(false);
            }
            else
            {
                result = await hub.SendTemplateNotificationAsync(templateParams, "$InstallationId:{" + installationId + "}").ConfigureAwait(false);
            }


            // Write the success result to the logs.
            config.Services.GetTraceWriter().Info(result.State.ToString());
            return result;
        }
    }
}
