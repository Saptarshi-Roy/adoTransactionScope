using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Transactions;
using System.Data.SqlClient;

namespace AdoConsoleTScope
{
    class Program
    {
        static void Main(string[] args)
        {try
            {
                using (TransactionScope ts1 = new TransactionScope()) //////////////////////////////////              distributed transaction.
                {
                    using (SqlConnection con = new SqlConnection("data source=PC440352;initial catalog=aspnetSample;integrated security=true"))
                    {
                        con.Open();
                        using (SqlCommand cmd = new SqlCommand("insert into student values(10,'rahul','agartala',50)",con))
                        {
                            int res = cmd.ExecuteNonQuery();
                            if(res>0)
                            {
                                using (SqlConnection con1 = new SqlConnection("data source=PC440352;initial catalog=aspnetSample;integrated security=true"))
                                {
                                    con1.Open();
                                    using (SqlCommand cmd1 = new SqlCommand("update student set sname='bhushan',location='Delhi',age=45 where stid=5", con1))
                                    {
                                        int res1 = cmd1.ExecuteNonQuery();
                                        if(res1>0)
                                        {
                                            ts1.Complete();
                                            Console.WriteLine("Transaction Completed");
                                            con1.Close();
                                           
                                        }
                                    }
                                }
                            }
                        }
                        con.Close();
                    }
                }
            }
            catch(Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            Console.ReadLine();
        }
    }
}
