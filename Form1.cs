using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Xml.Linq;

namespace QuanLyDienJAV
{
    public partial class Form1 : Form
    {
        private XDocument xmldoc;
        private string path = "DienVienJAV.xml";
        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            LoadDataModelJAV();
        }
        private void ClearDataBiding()
        {
            txt_id.DataBindings.Clear();
            txt_fullname.DataBindings.Clear();
            txt_YearofBirth.DataBindings.Clear();
            txt_film.DataBindings.Clear();
            txt_nationality.DataBindings.Clear();
        }
        public void LoadDataModelJAV()
        {
            xmldoc = XDocument.Load(path);
            var data = xmldoc.Descendants("ModelJAV").Select(p => new
            {
                Id = p.Element("id").Value,
                FullName = p.Element("fullname").Value,
                YearofBirth = p.Element("YearofBirth").Value,
                film = p.Element("film").Value,
                nationality = p.Element("nationality").Value
            }).OrderBy(p => p.Id).ToList();
            //lam sach du lieu
            ClearDataBiding();
            // them du lieu vao dtgv
            txt_id.DataBindings.Add("text", data, "id");
            txt_fullname.DataBindings.Add("text", data, "fullname");
            txt_YearofBirth.DataBindings.Add("text", data, "YearofBirth");
            txt_film.DataBindings.Add("text", data, "film");
            txt_nationality.DataBindings.Add("text", data, "nationality");
            dtgvModelJav.DataSource = data;
        }

        private void btn_add_Click(object sender, EventArgs e)
        {
            foreach (var item in group_info.Controls)
            {
                if (item is TextBox)
                {
                    (item as TextBox).Clear();
                }
            }
            txt_id.Focus();
        }

        private void btn_save_Click(object sender, EventArgs e)
        {
            XElement models = new XElement("ModelJAV",
                new XElement("id", txt_id.Text),
                new XElement("fullname", txt_fullname.Text),
                new XElement("YearofBirth", txt_YearofBirth.Text),
                new XElement("film", txt_film.Text),
                new XElement("nationality", txt_nationality.Text));
            xmldoc.Root.Add(models);
            xmldoc.Save(path);
            LoadDataModelJAV();
            btn_add_Click(null, null);
        }

        private void btn_update_Click(object sender, EventArgs e)
        {
            XElement models = xmldoc.Descendants("Employee").FirstOrDefault(p => p.Element("id").Value == txt_id.Text);
            if (models != null)
            {
                models.Element("fullname").Value = txt_fullname.Text;
                models.Element("YearofBirth").Value = txt_YearofBirth.Text;
                models.Element("film").Value = txt_film.Text;
                models.Element("nationality").Value = txt_nationality.Text;
                xmldoc.Save(path);
                LoadDataModelJAV();
                btn_add_Click(null, null);
            }
        }

        private void btn_delete_Click(object sender, EventArgs e)
        {
            XElement models = xmldoc.Descendants("ModelJAV").FirstOrDefault(p => p.Element("id").Value == txt_id.Text);
            if (models != null)
            {
                models.Remove();
                xmldoc.Save(path);
                LoadDataModelJAV();
                btn_add_Click(null, null);
            }
        }
    }
}
