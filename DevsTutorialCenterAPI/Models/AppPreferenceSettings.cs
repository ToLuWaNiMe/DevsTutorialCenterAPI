using System;
namespace DevsTutorialCenterAPI.Models
{
	public class AppPreferenceSettings
	{
		public List<SettingsObj> GeneralSettings { get; set; } = new List<SettingsObj>
        {
            
            new SettingsObj
            {
                Category = "Communication Settings",
                SubCategory = new Dictionary<string, bool>
                {
                    { "Email Notification", true }
                }
            },

            new SettingsObj
            {
                Category = "Theme and Appearance",
                SubCategory = new Dictionary<string, bool>
                {
                    { "Theme Selection", true },
                    { "Customization", true }
                }
            },

            new SettingsObj
            {
                Category = "Security Settings",
                SubCategory = new Dictionary<string, bool>
                {
                    { "Two-Factor Authentication", false },
                    { "Password Update", true }
                }
            },

            new SettingsObj
            {
                Category = "Privacy Settings",
                SubCategory = new Dictionary<string, bool>
                {
                    { "Profile Visibility", true },
                    { "Email Privacy", false }
                }
            },

            new SettingsObj
            {
                Category = "Content Management",
                SubCategory = new Dictionary<string, bool>
                {
                    { "Categories and Tags", false },
                    { "Featured Posts", true }
                }
            },

            new SettingsObj
            {
                Category = "Account Deactivation",
                SubCategory = new Dictionary<string, bool>
                {
                    { "Deactivate Account", true }
                }
            },

        };

	}

	public class SettingsObj
	{
        public string Category { get; set; } = "";
        public Dictionary<string, bool> SubCategory { get; set; } =  new Dictionary<string, bool>();
    }
}

