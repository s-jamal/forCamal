using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace CrudWithEntityFramwork
{
    public partial class Form1 : Form
    {
        private NewsEntities db = new NewsEntities();
        private Category selectedCategory;
        private News selectedNews;
        public Form1()
        {
            InitializeComponent();
            fillCategoryData();
            fillNewsData();
            fillCategoryCombobox();
        }

        private void fillCategoryData()
        {
            dgwCategories.DataSource = db.Categories.ToList();
        }

        private void resetCategory()
        {
            fillCategoryData();
            txtCategoryName.Clear();
            btnAddCategory.Visible = true;
            btnUpdateCategory.Visible = false;
            btnDeleteCategory.Visible = false;
        }

        private void btnAddCategory_Click(object sender, EventArgs e)
        {
            Category cnt = new Category();
            cnt.name = txtCategoryName.Text;
            db.Categories.Add(cnt);
            db.SaveChanges();

            fillCategoryData();
            txtCategoryName.Clear();
        }

        private void dgwCategories_RowHeaderMouseDoubleClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            int id = Convert.ToInt32(dgwCategories.Rows[e.RowIndex].Cells[0].Value.ToString());
            this.selectedCategory = db.Categories.Find(id);

            txtCategoryName.Text = this.selectedCategory.name.Trim();

            btnAddCategory.Visible = false;
            btnUpdateCategory.Visible = true;
            btnDeleteCategory.Visible = true;
        }

        private void btnDeleteCategory_Click(object sender, EventArgs e)
        {
            db.Categories.Remove(this.selectedCategory);
            db.SaveChanges();
            resetCategory();
        }

        private void btnUpdateCategory_Click(object sender, EventArgs e)
        {
            this.selectedCategory.name = txtCategoryName.Text;
            db.SaveChanges();
            resetCategory();
        }

        private void fillNewsData()
        {
            dgwNews.DataSource = db.News.ToList();
        }

        private void fillCategoryCombobox()
        {
            List<Category> list = db.Categories.ToList();
            foreach(Category item in list)
            {
                cmbNewsCategory.Items.Add(item.name.Trim());
            }
        }

        public void resetNews()
        {
            txtNewsTitle.Clear();
            dptNewsDate.ResetText();
            cmbNewsCategory.ResetText();
            rtbNewsDesc.Clear();
            rtbNewsText.Clear();
            fillNewsData();
            btnAddNews.Visible = true;
            btnUpdateNews.Visible = false;
            btnDeleteNews.Visible = false;
        }

        private void btnAddNews_Click(object sender, EventArgs e)
        {
            News nws = new News();
            nws.title = txtNewsTitle.Text;
            nws.date = dptNewsDate.Value;
            nws.category_id = db.Categories.FirstOrDefault(c => c.name == cmbNewsCategory.Text).id;
            nws.news_desc = rtbNewsDesc.Text;
            nws.news_text = rtbNewsText.Text;

            db.News.Add(nws);
            db.SaveChanges();
            resetNews();
        }

        private void dgwNews_RowHeaderMouseDoubleClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            int id = Convert.ToInt32(dgwNews.Rows[e.RowIndex].Cells[0].Value.ToString());
            this.selectedNews = db.News.Find(id);

            txtNewsTitle.Text = this.selectedNews.title.Trim() ;
            dptNewsDate.Value = Convert.ToDateTime(this.selectedNews.date);
            cmbNewsCategory.Text = this.selectedNews.Category.name.Trim();
            rtbNewsDesc.Text = this.selectedNews.news_desc;
            rtbNewsText.Text = this.selectedNews.news_text;
            btnAddNews.Visible = false;
            btnUpdateNews.Visible = true;
            btnDeleteNews.Visible = true;
        }

        private void btnDeleteNews_Click(object sender, EventArgs e)
        {
            db.News.Remove(this.selectedNews);
            db.SaveChanges();
            resetNews();
        }

        private void btnUpdateNews_Click(object sender, EventArgs e)
        {
            this.selectedNews.title = txtNewsTitle.Text;
            this.selectedNews.date = dptNewsDate.Value;
            this.selectedNews.category_id = db.Categories.FirstOrDefault(c => c.name == cmbNewsCategory.Text).id;
            this.selectedNews.news_desc = rtbNewsDesc.Text;
            this.selectedNews.news_text = rtbNewsText.Text;
            db.SaveChanges();
            resetNews();
        }
    }
}
