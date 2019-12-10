using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PMSEntity;
using PMSException;
using PMSBLL;
using ConsoleTables;
using ConsoleTableExt;

namespace ProductMgmtSystem
{
    class Program
    {
        static void Main(string[] args)
        {
            ProductBLL.Setlist();
            int choice;
            //   Console.WriteLine(Directory.GetCurrentDirectory() + "");
            do
            {
                PrintMenu();
                Console.Write("Enter Choice :");
                choice = Int32.Parse(Console.ReadLine());
                switch (choice)
                {
                    case 1:
                        AddProduct();
                        break;
                    case 2:
                        ShowDetails();
                        break;
                    case 3:
                        Delete();
                        break;
                    case 4:
                        SearchID();
                        break;
                    case 5:
                        UpdateProduct();
                        break;
                    default:
                        break;
                }
            } while ((choice > 0 && choice < 6));
        }

        private void Window_Closing(object sender, System.ComponentModel.CancelEventArgs e)
            { SetSerialization(); }
        private static void SetSerialization()
        {
           ProductBLL.SetSerialization();
        }

        private static void PrintMenu()
        {
            Console.WriteLine("\n***********Product Management System ***********");
            Console.WriteLine("1. Add Product");
            Console.WriteLine("2. Show All Products");
            Console.WriteLine("3. Delete Product");
            Console.WriteLine("4. Search Product");
            Console.WriteLine("5. Update Product");
            Console.WriteLine("**************************************************");
        }

        private static void AddProduct()
        {
            try
            {
                ProductEntity pro = new ProductEntity();
                Console.Write("Enter Product ID :");
                pro.ProductID = Convert.ToInt32(Console.ReadLine());
                Console.Write("Enter Product Name :");
                pro.ProductName = Console.ReadLine();
                Console.Write("Enter Product Category :");
                pro.ProductCategory = Console.ReadLine();
                Console.Write("Enter Price :");
                pro.ProductPrice = Double.Parse(Console.ReadLine());
                bool productAdded = ProductBLL.AddProductBLL(pro);
                if (productAdded)
                    Console.WriteLine("===================\nProduct Added\n==================");
                else
                    Console.WriteLine("Product not Added");
            }
            catch (ProductException ex)
            {
                Console.WriteLine(ex.Message);
            }
        }

        private static void ShowDetails()
        {
            try
            {
                List<ProductEntity> pmsList = ProductBLL.ShowDetailsBLL();
                if (pmsList != null && pmsList.Count > 0)
                {
                    ConsoleTable.From<ProductEntity>(pmsList).Write();

                    //Console.WriteLine("******************************************************************************");
                    //Console.WriteLine("Product ID\tProduct Name\tProduct Category\tPrice");
                    //Console.WriteLine("******************************************************************************");
                    //foreach (ProductEntity pro in pmsList)
                    //{
                    //    Console.WriteLine("\t{0}\t{1}\t\t{2}\t\t{3}", pro.ProductID, pro.ProductName, pro.ProductCategory, pro.ProductPrice);
                    //}
                    Console.WriteLine("********************************************************");

                }
                else
                {
                    Console.WriteLine("No Product Details Available");
                }
            }
            catch (ProductException ex)
            {
                Console.WriteLine(ex.Message);
            }
        }

        private static void UpdateProduct()
        {
            try
            {
                int updateID;
                Console.WriteLine("Enter Product ID to Update Details:");
                updateID = Convert.ToInt32(Console.ReadLine());
                ProductEntity updated = ProductBLL.SearchBL(updateID);
                if (updated != null)
                {
                    Console.WriteLine("Update Product Name :");
                    updated.ProductName = Console.ReadLine();
                    Console.WriteLine("Update Product Category :");
                    updated.ProductCategory = Console.ReadLine();
                    Console.WriteLine("Update Product Price :");
                    updated.ProductPrice = Double.Parse(Console.ReadLine());
                    bool Updated = ProductBLL.UpdateBL(updated);
                    if (Updated)
                        Console.WriteLine("Product Details Updated");
                    else
                        Console.WriteLine("PRoduct Details not Updated ");
                }
                else
                {
                    Console.WriteLine("No Product Details Available");
                }


            }
            catch (ProductException ex)
            {
                Console.WriteLine(ex.Message);
            }
        }

        private static void SearchID()
        {
            try
            {
                int searchID;
                Console.Write("Enter Product ID to Search:");
                searchID = Int32.Parse(Console.ReadLine());
                ProductEntity search = ProductBLL.SearchBL(searchID);
            
                if (search != null)
                {
                    Console.WriteLine("**************************************");
                    Console.WriteLine("Product ID\t\tProduct Name\t\tCategory\t\tPrice");
                    Console.WriteLine("**************************************");
                    Console.WriteLine("{0}\t\t{1}\t\t{2}\t\t{3}", search.ProductID, search.ProductName, search.ProductCategory,search.ProductPrice);
                    Console.WriteLine("***************************************");
                }
                else
                {
                    Console.WriteLine("No Product Details Available");
                }

            }
            catch (ProductException ex)
            {
                Console.WriteLine(ex.Message);
            }
        }

        private static void Delete()
        {
            try
            {
                int deleteID;
                Console.WriteLine("Enter Product ID to Delete:");
                deleteID = Convert.ToInt32(Console.ReadLine());
                bool deleted = ProductBLL.DeleteBL(deleteID);
                if (deleted)
                    Console.WriteLine("Product Deleted");
                else
                    Console.WriteLine("Product not Deleted ");


            }
            catch (ProductException ex)
            {
                Console.WriteLine(ex.Message);
            }
        }
    }
}

