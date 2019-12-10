using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.Serialization.Formatters.Binary;
using System.Text;
using System.Threading.Tasks;
using PMSEntity;
using PMSException;

namespace PMSDAL
{
    public class ProductDAL
    {
        public static List<ProductEntity> PMSList = new List<ProductEntity>();
        public static string fileName = "ProductList";

        public static bool AddProductDAL(ProductEntity product)
        {
            bool productAdded = false;
            try
            {
                PMSList.Add(product);
                productAdded = true;
                SetSerialization();
            }
            catch (Exception ex)
            {
                throw new ProductException(ex.Message);
            }
            return productAdded;

        }

        public static List<ProductEntity> ShowDetailsDAL()
        {
            return PMSList;
        }

        public static bool DeleteProductDAL(int deleteID)
        {
            bool productDeleted = false;
            try
            {
                for (int i = 0; i < PMSList.Count; i++)
                {
                    ProductEntity pro = PMSList[i];
                    if (pro.ProductID == deleteID)
                    {
                        PMSList.RemoveAt(i);
                        productDeleted = true;
                        SetSerialization();
                        break;
                    }
                }

            }
            catch (Exception ex)
            {
                throw new ProductException(ex.Message);
            }
            return productDeleted;
        }

        public static ProductEntity SearchDAL(int searchID)
        {
            ProductEntity searchid = null;
            try
            {


                for (int i = 0; i < PMSList.Count; i++)
                {
                    ProductEntity pro = PMSList[i];
                    if (pro.ProductID == searchID)
                    {
                        searchid = PMSList[i];
                        break;
                    }
                }
            }
            catch (Exception ex)
            {
                throw new ProductException(ex.Message);
            }
            return searchid;
        }

        public static bool UpdateDAL(ProductEntity update)
        {
            bool Updated = false;
            try
            {
                for (int i = 0; i < PMSList.Count; i++)
                {
                    ProductEntity pro =PMSList[i];
                    if (pro.ProductID == update.ProductID)
                    {
                        PMSList[i] = update;
                        SetSerialization();
                        break;
                    }
                }
                Updated = true;
            }
            catch (Exception ex)
            {
                throw new ProductException(ex.Message);
            }
            return Updated;
        }

        public static void SetSerialization()
        {
            try
            {
                using (Stream file = File.Open(fileName, FileMode.Create))
                {
                    BinaryFormatter bf = new BinaryFormatter();
                    bf.Serialize(file, PMSList);
                    file.Close();
                }
            }
            catch (FileNotFoundException)
            {
                Console.WriteLine("File not found.");
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }

        }

        //Setting List if the file already exists
        public static void SetList()
        {
            DeserializeFile();
        }

        public static void DeserializeFile()
        {
            try
            {
                using (Stream file = File.Open(Directory.GetCurrentDirectory() + "\\" + fileName, FileMode.Open))
                {
                    BinaryFormatter bf = new BinaryFormatter();
                    PMSList = bf.Deserialize(file) as List<ProductEntity>;
                    file.Close();
                }
            }
            catch (FileNotFoundException ex)
            {
                Console.WriteLine(ex.Message);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
         
        }
    }
}

