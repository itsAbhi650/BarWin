using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BarWin
{
    public static class QRCodeContentFormatter
    {
        /// <summary>
        /// Generate WiFi QR Code Format
        /// </summary>
        /// <param name="ssid"></param>
        /// <param name="password"></param>
        /// <param name="encryption"></param>
        /// <param name="hidden"></param>
        /// <returns></returns>
        public static string GenerateWiFi(string ssid, string password, string encryption = "WPA", bool hidden = false)
        {
            return $"WIFI:S:{ssid};T:{encryption};P:{password};H:{(hidden ? "true" : "false")};;";
        }

        /// <summary>
        ///  Generate vCard QR Code Format
        /// </summary>
        /// <param name="name"></param>
        /// <param name="phone"></param>
        /// <param name="email"></param>
        /// <param name="address"></param>
        /// <param name="org"></param>
        /// <param name="url"></param>
        /// <returns></returns>
        public static string GenerateVCard(string name, string phone, string email, string address, string org = "", string url = "")
        {
            return $"BEGIN:VCARD\nVERSION:3.0\nFN:{name}\nORG:{org}\nTEL:{phone}\nEMAIL:{email}\nADR:{address}\nURL:{url}\nEND:VCARD";
        }

        /// <summary>
        /// Generate BizCard QR Code Format (Compact vCard alternative)
        /// </summary>
        /// <param name="firstName"></param>
        /// <param name="lastName"></param>
        /// <param name="phone"></param>
        /// <param name="email"></param>
        /// <param name="company"></param>
        /// <returns></returns>
        public static string GenerateBizCard(string firstName, string lastName, string phone, string email, string company)
        {
            return $"BIZCARD:N:{lastName},{firstName};TEL:{phone};EMAIL:{email};ORG:{company};;";
        }

        /// <summary>
        /// Generate Telephone Number QR Code Format
        /// </summary>
        /// <param name="phoneNumber"></param>
        /// <returns></returns>
        public static string GenerateTelephone(string phoneNumber)
        {
            return $"tel:{phoneNumber}";
        }

        /// <summary>
        /// Generate URL QR Code Format
        /// </summary>
        /// <param name="url"></param>
        /// <returns></returns>
        public static string GenerateURL(string url)
        {
            return url.StartsWith("http") ? url : $"http://{url}";
        }

        /// <summary>
        /// Generate Email QR Code Format
        /// </summary>
        /// <param name="email"></param>
        /// <param name="subject"></param>
        /// <param name="body"></param>
        /// <returns></returns>
        public static string GenerateEmail(string email, string subject = "", string body = "")
        {
            return $"mailto:{email}?subject={Uri.EscapeDataString(subject)}&body={Uri.EscapeDataString(body)}";
        }

        /// <summary>
        /// Generate Play Store App Link
        /// </summary>
        /// <param name="packageName"></param>
        /// <returns></returns>
        public static string GeneratePlayStoreLink(string packageName)
        {
            return $"https://play.google.com/store/apps/details?id={packageName}";
        }

        /// <summary>
        /// Generate SMS QR Code Format
        /// </summary>
        /// <param name="phoneNumber"></param>
        /// <param name="message"></param>
        /// <returns></returns>
        public static string GenerateSMS(string phoneNumber, string message = "")
        {
            return $"SMSTO:{phoneNumber}:{message}";
        }

        /// <summary>
        /// Generate Geo Location QR Code Format
        /// </summary>
        /// <param name="latitude"></param>
        /// <param name="longitude"></param>
        /// <param name="zoom"></param>
        /// <returns></returns>
        public static string GenerateGeoLocation(double latitude, double longitude, double zoom = 15)
        {
            return $"geo:{latitude},{longitude}?z={zoom}";
        }

        /// <summary>
        /// Generate Calendar Event QR Code Format (iCalendar)
        /// </summary>
        /// <param name="eventName"></param>
        /// <param name="startTime"></param>
        /// <param name="endTime"></param>
        /// <param name="location"></param>
        /// <param name="description"></param>
        /// <returns></returns>
        public static string GenerateCalendarEvent(string eventName, DateTime startTime, DateTime endTime, string location = "", string description = "")
        {
            return $"BEGIN:VEVENT\nSUMMARY:{eventName}\nDTSTART:{startTime:yyyyMMddTHHmmssZ}\nDTEND:{endTime:yyyyMMddTHHmmssZ}\nLOCATION:{location}\nDESCRIPTION:{description}\nEND:VEVENT";
        }

        /// <summary>
        /// Generate WhatsApp Direct Chat QR Code Format
        /// </summary>
        /// <param name="phoneNumber"></param>
        /// <param name="message"></param>
        /// <returns></returns>
        public static string GenerateWhatsApp(string phoneNumber, string message = "")
        {
            return $"https://wa.me/{phoneNumber}?text={Uri.EscapeDataString(message)}";
        }
    }
}
