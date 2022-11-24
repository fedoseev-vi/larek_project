using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Threading;
using System.Threading.Tasks;
using System.Net.Http;
using System.Net;
using System.Diagnostics;
using larek_client.Models;
using System.Net.Http.Json;


namespace larek_client
{
    public partial class Form1 : Form
    {
        static Product[] SearchResult = null;
        static Order[] AllOrders = null;
        static Brand[] AllBrands = null;
        static Category[] AllCategories = null;
        static int ProductInOrder = -1;
        static Order OrderToSend = new Order();

        public Form1()
        {
            InitializeComponent();
            comboBox1.SelectedIndex = 0;
            OrderToSend.status_id = 1;
        }

        private static async Task GetAllProducts()
        {
            var client = new HttpClient();
            try
            {
                SearchResult = await client.GetFromJsonAsync<Product[]>("https://localhost:5001/api/ProductsApi");
            }
            catch(Exception ex)
            {
                MessageBox.Show(ex.Message, "Ошибка");
            }
        }

        private static async Task GetProductsByBrand(int i)
        {
            var client = new HttpClient();
            try
            {
                SearchResult = await client.GetFromJsonAsync<Product[]>("https://localhost:5001/api/ProductsApi/BrandId/" + i.ToString());
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Ошибка");
            }
        }

        private static async Task GetProductsByCategory(int i)
        {
            var client = new HttpClient();
            try
            {
                SearchResult = await client.GetFromJsonAsync<Product[]>("https://localhost:5001/api/ProductsApi/CategoryId/" + i.ToString());
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Ошибка");
            }
        }

        private static async Task GetProductsByNamePart(string s)
        {
            var client = new HttpClient();
            try
            {
                SearchResult = await client.GetFromJsonAsync<Product[]>("https://localhost:5001/api/ProductsApi/NamePart/" + s);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Ошибка");
            }
        }

        private static async Task GetAllOrders()
        {
            var client = new HttpClient();
            try
            {
                AllOrders = await client.GetFromJsonAsync<Order[]>("https://localhost:5002/api/Orders");
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Ошибка");
            }
        }

        private static async Task GetAllBrands()
        {
            var client = new HttpClient();
            try
            {
                AllBrands = await client.GetFromJsonAsync<Brand[]>("https://localhost:5001/api/BrandsApi");
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Ошибка");
            }
        }

        private static async Task GetAllCategories()
        {
            var client = new HttpClient();
            try
            {
                AllCategories = await client.GetFromJsonAsync<Category[]>("https://localhost:5001/api/CategoriesApi");
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Ошибка");
            }
        }

        private async void button2_Click(object sender, EventArgs e)
        {
            ProductSearchGrid.DataSource = null;
            switch (comboBox1.SelectedIndex)
            {
                case 1:
                    {
                        SearchBox.Enabled = false;
                        comboBoxSearch.Enabled = true;
                        await GetProductsByBrand(comboBoxSearch.SelectedIndex+1);
                        break;
                    }
                case 2:
                    {
                        await GetProductsByCategory(comboBoxSearch.SelectedIndex+1);
                        break;
                    }
                default:
                    {
                        if (SearchBox.Text == "")
                        {
                            await GetAllProducts();
                        }
                        else
                        {
                            await GetProductsByNamePart(SearchBox.Text);
                        }
                        break;
                    }
            }
            ProductSearchGrid.Refresh();
            ProductSearchGrid.DataSource = SearchResult;

        }

        private static async Task PostOrder(Order order)
        {
            var client = new HttpClient();
            try
            {
                await client.PostAsJsonAsync<Order>("https://localhost:5002/api/Orders", order);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Ошибка");
            }
        }

        private async void button3_Click(object sender, EventArgs e)
        {
            await GetAllOrders();
            OrdersGrid.Refresh();
            OrdersGrid.DataSource = AllOrders;
        }

        private async void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            switch (comboBox1.SelectedIndex)
            {
                case 1:
                    {
                        SearchBox.Enabled = false;
                        comboBoxSearch.Enabled = true;
                        comboBoxSearch.Items.Clear();
                        await GetAllCategories();
                        for(int i=0; i < AllCategories.Length; i++)
                        {
                            comboBoxSearch.Items.Add(AllCategories[i].category_name);
                        }
                        comboBoxSearch.SelectedIndex = 0;
                        break;
                    }
                case 2:
                    {
                        SearchBox.Enabled = false;
                        comboBoxSearch.Enabled = true;
                        comboBoxSearch.Items.Clear();
                        await GetAllBrands();
                        for (int i = 0; i < AllBrands.Length; i++)
                        {
                            comboBoxSearch.Items.Add(AllBrands[i].brand_name);
                        }
                        comboBoxSearch.SelectedIndex = 0;
                        break;
                    }
                default:
                    {
                        SearchBox.Enabled = true;
                        comboBoxSearch.Enabled = false;
                        comboBoxSearch.Items.Clear();
                        break;
                    }
            }
        }

        private void AddToOrderButton_Click(object sender, EventArgs e)
        {
            if(ProductSearchGrid.SelectedRows.Count != 0)
            {
                ProductInOrder = int.Parse(ProductSearchGrid.SelectedRows[0].Cells[0].Value.ToString());
                CartLabel.Text = "В корзине есть товар";
            }
        }

        private void buttonClearCart_Click(object sender, EventArgs e)
        {
            ProductInOrder = -1;
            CartLabel.Text = "Товар не выбран";
        }

        private async void buttonPlaceOrder_Click(object sender, EventArgs e)
        {
            if (NameBox.Text != "" && AdressBox.Text != "" && ProductInOrder != -1)
            {
                OrderToSend.product = ProductInOrder;
                OrderToSend.customer_name = NameBox.Text;
                OrderToSend.adress = AdressBox.Text;
                await PostOrder(OrderToSend);
                OrderToSend = null;
                button3_Click(sender, e);
            }
        }
    }
}
