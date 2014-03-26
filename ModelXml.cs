/*
Copyright (c) 2011 Ben Barron, 2014 gridgem

Permission is hereby granted, free of charge, to any person obtaining a copy 
of this software and associated documentation files (the "Software"), to deal 
in the Software without restriction, including without limitation the rights 
to use, copy, modify, merge, publish, distribute, sublicense, and/or sell 
copies of the Software, and to permit persons to whom the Software is furnished 
to do so, subject to the following conditions:

The above copyright notice and this permission notice shall be included in all 
copies or substantial portions of the Software.

THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR IMPLIED, 
INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF MERCHANTABILITY, FITNESS FOR A 
PARTICULAR PURPOSE AND NONINFRINGEMENT. IN NO EVENT SHALL THE AUTHORS OR COPYRIGHT 
HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER LIABILITY, WHETHER IN AN ACTION 
OF CONTRACT, TORT OR OTHERWISE, ARISING FROM, OUT OF OR IN CONNECTION WITH THE 
SOFTWARE OR THE USE OR OTHER DEALINGS IN THE SOFTWARE.

Hibernation Code provided and integrated by Miljbee (miljbee@gmail.com)
*/

using System;
using System.Collections.Generic;

using System.Windows.Forms;
using System.IO;
using System.Xml;
using System.Xml.Serialization;

using WindowsAPI;

namespace PS3BluMote
{
    public class SettingsNode
    {
        public string vendorid;
        public string productid;
        public bool smsinput;
        public int repeatinterval;
        public bool debug;
        public string isound;
        public string rsound;
        public bool osd;

        public SettingsNode()
        {
            vendorid = "0x054c";
            productid = "0x0306";
            smsinput = false;
            repeatinterval = 500;
            debug = false;
            isound = "";
            rsound = "";
            osd = false;
        }
    }

    public class OsdNode
    {
        public string align;
        public string valign;
        public bool xysetting;
        public int pos_x;
        public int pos_y;

        public string fontFamily;
        public int fontSize;
        public string fontStyle;

        public string textColor;
        public string pathColor;
        public Single pathWidth;

        public byte alpha;
        public int textTime;
        public string animateEffect;
        public uint animateTime;
        public string testString;

        public OsdWhen osdWhen;

        public OsdNode()
        {
            align = "Center";
            valign = "Bottom";
            xysetting = false;
            pos_x = 200;
            pos_y = 200;

            fontFamily = "Arial";
            fontSize = 48;
            fontStyle = "Bold";

            textColor = "Black";
            pathColor = "White";
            pathWidth = 3.0f;

            alpha = 192;
            textTime = 2200;
            animateEffect = "SlideTopToBottom";
            animateTime = 400;
            testString = "This is a test.";

            osdWhen = new OsdWhen();
        }
    }

    public class OsdWhen
    {
        public bool appStart;
        public bool remoteConnect;
        public bool remoteDisconnect;
        public bool remoteHibernate;
        public bool remoteBatteryChange;

        public bool remoteButtonPressed;
        public bool remoteButtonPressedAlways;
        public bool remoteButtonPressedMatched;
        public bool remoteButtonPressedAssigned;

        public bool activeWindowTitle;
        public bool mappingName;
        public bool pressedRemoteButton;
        public bool assignedKey;

        public OsdWhen()
        {
            appStart = true;
            remoteConnect = false;
            remoteDisconnect = false;
            remoteHibernate = true;
            remoteBatteryChange = true;

            remoteButtonPressed = true;
            remoteButtonPressedAlways = true;
            remoteButtonPressedMatched = false;
            remoteButtonPressedAssigned = false;

            activeWindowTitle = true;
            mappingName = true;
            pressedRemoteButton = true;
            assignedKey = true;
        }
    }

    public class AppNode
    {
        [XmlAttribute("name")]
        public string name;
        [XmlAttribute("condition")]
        public string condition;
        [XmlAttribute("caseSensitive")]
        public bool caseSensitive;
        [XmlElement("button")]
        public ButtonMapping[] buttonMappings;

        public AppNode()
        {
            name = "General";
            condition = ".*";
            caseSensitive = false;
            buttonMappings = new ButtonMapping[56];

            string[] buttonNames = Enum.GetNames(typeof(PS3Remote.Button));
            for (int i = 0; i < buttonNames.Length; i++)
            {
                buttonMappings[i] = new ButtonMapping();
                buttonMappings[i].name = buttonNames[i];
            }
        }
    }

    public class ButtonMapping
    {
        [XmlAttribute("name")]
        public string name;
        [XmlAttribute("repeat")]
        public bool repeat;
        [XmlIgnore]
        public List<SendInputAPI.Keyboard.KeyCode> keysMapped;

        [XmlText]
        public string joinedKeyMapped
        {
            get
            {
                return string.Join(",", keysMapped);
            }
            set
            {
                foreach (string keyCode in value.Split(','))
                {
                    keyCode.Replace("Menu", "Alt"); // 2.0 to 2.2
                    keysMapped.Add((SendInputAPI.Keyboard.KeyCode)Enum.Parse(typeof(SendInputAPI.Keyboard.KeyCode), keyCode, true));
                }
            }
        }

        public ButtonMapping()
        {
            name = "";
            repeat = false;
            keysMapped = new List<SendInputAPI.Keyboard.KeyCode>();
        }
    }

    // ----------
    [XmlRoot("PS3BluMote")]
    public class ModelXml
    {
        private string SettingsFile;

        [XmlAttribute("version")]
        public String SETTINGS_VERSION = "2.2";

        [XmlElement("settings")]
        public SettingsNode Settings;

        [XmlElement("OSD")]
        public OsdNode OSD;

        [XmlArray("mappings")]
        [XmlArrayItem("app")]
        public List<AppNode> Mappings;

        [XmlArray("app")]
        [XmlArrayItem("button")]
        public AppNode[] Apps;

        // ----------
        private ModelXml(){}

        public ModelXml(string filePath)
        {
            SettingsFile = filePath;

            Settings = new SettingsNode();
            OSD = new OsdNode();
            Mappings = new List<AppNode>();
            Mappings.Add(new AppNode());

            Load();
        }

        private void Load()
        {
            try
            {
                XmlSerializer serializer = new XmlSerializer(typeof(ModelXml));

                FileStream fileStream = new FileStream(SettingsFile, FileMode.Open);
                ModelXml rootNode = (ModelXml)serializer.Deserialize(fileStream);
                fileStream.Close();
            
                // version check
                XmlDocument document = new XmlDocument();
                document.Load(SettingsFile);

                String verNumber = document.GetElementsByTagName("PS3BluMote")[0].Attributes["version"].Value;
                if (verNumber == "2.0")
                {
                    ModelXml20 modelXml20 = new ModelXml20(SettingsFile);

                    Settings = rootNode.Settings;
                    Mappings[0].buttonMappings = modelXml20.Mappings;

                    // backup 2.0.ini
                    try
                    {
                        DirectoryInfo SettingsDir = Directory.GetParent(SettingsFile);
                        File.Copy(SettingsFile, SettingsDir + "\\settings2.0.ini");
                        MessageBox.Show(
                            "Converted setting file (2.0 to 2.2) and the original file is saved as \"setting2.0.ini\".",
                            "PS3BluMote: Message",
                            MessageBoxButtons.OK
                        );
                    }
                    catch
                    {
                        MessageBox.Show(
                            "An error occured while attempting to backup 2.0 ini file.\nmaybe already there's a same name file.\n(\"settings2.0.ini\")",
                            "PS3BluMote: Remote error!",
                            MessageBoxButtons.OK,
                            MessageBoxIcon.Error
                        );
                    }
                    Save();
                }
                else
                {
                    Settings = rootNode.Settings;
                    OSD = rootNode.OSD;
                    Mappings = rootNode.Mappings;
                }
            }
            catch
            {
                Save();
            }
        }

        public void Save()
        {
            /*
            try
            {
            */
                XmlSerializer serializer = new XmlSerializer(typeof(ModelXml));

                XmlSerializerNamespaces xmlNameSpaces = new XmlSerializerNamespaces();
                xmlNameSpaces.Add(String.Empty, String.Empty);

                DirectoryInfo SettingsDir = Directory.GetParent(SettingsFile);
                if (!SettingsDir.Exists)
                {
                    Directory.CreateDirectory(SettingsDir.FullName);
                }

                TextWriter writer = new StreamWriter(SettingsFile);
                serializer.Serialize(writer, this, xmlNameSpaces);
                writer.Close();
            /*
            }
            catch
            {
                MessageBox.Show(
                    "An error occured whilst attempting to load the remote.",
                    "PS3BluMote: Remote error!",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error
                );
            }
            */
        }
    }

    [XmlRoot("PS3BluMote")]
    public class ModelXml20
    {
        private string SettingsFile20;

        // [XmlAttribute("version")]
        // private String SETTINGS_VERSION = "2.0";

        [XmlElement("settings")]
        private SettingsNode Settings;

        [XmlElement("OSD")]
        private OsdNode OSD;

        [XmlArray("mappings")]
        [XmlArrayItem("button")]
        public ButtonMapping[] Mappings;

        // ----------
        private ModelXml20() { }

        public ModelXml20(string filePath)
        {
            SettingsFile20 = filePath;

            Settings = new SettingsNode();
            OSD = new OsdNode();
            Mappings = new ButtonMapping[56];

            Load();
        }

        private void Load()
        {
            try
            {
                XmlSerializer serializer = new XmlSerializer(typeof(ModelXml20));

                FileStream fileStream = new FileStream(SettingsFile20, FileMode.Open);
                ModelXml20 rootNode = (ModelXml20)serializer.Deserialize(fileStream);
                fileStream.Close();

                Settings = rootNode.Settings;
                OSD = rootNode.OSD;
                Mappings = rootNode.Mappings;
            }
            catch
            {
                MessageBox.Show(
                    "An error occured while readning setting file (ver. 2.0).",
                    "PS3BluMote: Convert Error!",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error
                );
            }
        }
    }
}
