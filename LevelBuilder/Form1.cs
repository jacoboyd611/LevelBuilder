using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Xml;

namespace LevelBuilder
{
    public partial class Form1 : Form
    {   
        //this will rename the file according to the number put in
        int lvlNum = 1;

        #region for testing 
        Brush red = new SolidBrush(Color.Red);
        List<int> brickX = new List<int>();
        List<int> brickY = new List<int>();
        List<int> brickHeight = new List<int>();
        List<int> brickWidth = new List<int>();
        #endregion

        public Form1()
        {
            InitializeComponent();
            CreateXml();

            //testing purposes
            ReadXml();
            Refresh();
        }

        private void CreateXml()
        {
            //random setup shit that works?
            XmlDocument doc = new XmlDocument();
            XmlNode docNode = doc.CreateXmlDeclaration("1.0", "UTF-8", null);
            doc.AppendChild(docNode);

            //root node
            XmlNode bricksNode = doc.CreateElement("bricks");
            doc.AppendChild(bricksNode);

            foreach (Label label in Controls.OfType<Label>())
            {
                label.Visible = false;

                //single brick
                XmlNode brickNode = doc.CreateElement("brick");
                    //x
                    XmlAttribute xAttribute = doc.CreateAttribute("x");
                    xAttribute.Value = $"{label.Location.X}";
                    brickNode.Attributes.Append(xAttribute);
                    //y
                    XmlAttribute yAttribute = doc.CreateAttribute("y");
                    yAttribute.Value = $"{label.Location.Y}";
                    brickNode.Attributes.Append(yAttribute);
                    //width
                    XmlAttribute widthAttribute = doc.CreateAttribute("width");
                    widthAttribute.Value = $"{label.Width}";
                    brickNode.Attributes.Append(widthAttribute);
                    //height
                    XmlAttribute heightAttribute = doc.CreateAttribute("height");
                    heightAttribute.Value = $"{label.Height}";
                    brickNode.Attributes.Append(heightAttribute);
                    //value
                    XmlAttribute valueAttribute = doc.CreateAttribute("value");
                    valueAttribute.Value = $"{label.Text}";
                    brickNode.Attributes.Append(valueAttribute);
                    //colour
                    XmlAttribute colourAttribute = doc.CreateAttribute("colour");
                    colourAttribute.Value = $"{label.BackColor.Name}";
                    brickNode.Attributes.Append(colourAttribute);
                //close the brick node
                bricksNode.AppendChild(brickNode);
            }
            doc.Save($"lvl{lvlNum}.xml");
        }

        private void ReadXml()
        {
            XmlReader reader = XmlReader.Create($"lvl{lvlNum}.xml");
            while(reader.Read())
            {
                reader.ReadToFollowing("brick");
                brickX.Add(Convert.ToInt32(reader.GetAttribute("x")));
                brickY.Add(Convert.ToInt32(reader.GetAttribute("y")));
                brickWidth.Add(Convert.ToInt32(reader.GetAttribute("width")));
                brickHeight.Add(Convert.ToInt32(reader.GetAttribute("height")));
            }
        }

        private void Form1_Paint(object sender, PaintEventArgs e)
        {
            for(int i = 0; i<brickX.Count; i++) 
            { e.Graphics.FillRectangle(red, brickX[i], brickY[i], brickWidth[i], brickHeight[i]);  }
        }
    }
}
