using System;
using System.Collections.Generic;
using System.ComponentModel.Design;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MdiViewHelper;

namespace AllChatAI
{
    public static class GlobalVariable
    {
        //need to delete the file in order to use default values from the model
        public static string DefaultSettingFile { get; set; } = "settings.json";
        
        #region Do Not Edit
        public static ConfigurationModel ConfigurationModel { get; set; }

        public static void InitialiseSettingModel()
        {
            ConfigurationModel = ClassToJsonHelper.LoadFromFile<ConfigurationModel>(DefaultSettingFile);
            ClassToJsonHelper.SaveToFile(DefaultSettingFile, ConfigurationModel);
        }
        #endregion

    }
    public class ConfigurationModel
    {
        [TypeConverter(typeof(DefaultValueAttribute))]
        public string[] StartURLs { get; set; } = { "https://www.google.com", "https://www.yahoo.com.au" };

        [TypeConverter(typeof(ExpandableObjectConverter))]

        public MenuModel Menu { get; set; } = new MenuModel
        {
            MenuName = "Bookmarks",
            MenuURL = null,
            Children = new List<MenuModel>
            {
                new MenuModel
                {
                    MenuName = "New",
                    MenuURL = null,
                    Children = new List<MenuModel>
                    {
                        new MenuModel
                        {
                            MenuName = "Project",
                            MenuURL = "https://www.example.com/project",
                            Children = null
                        },
                        new MenuModel
                        {
                            MenuName = "File",
                            MenuURL = "https://www.example.com/newfile",
                            Children = null
                        }
                    }
                }
            }
        };
    }
}
