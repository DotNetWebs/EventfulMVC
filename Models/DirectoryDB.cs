using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Web.Configuration;
using ShoppersGuide;

namespace EventfulMVC.Models
{
    /// <summary>
    /// Summary description for DirectoryDB
    /// </summary>
    public class DirectoryDb
    {
        private string Connection
        {
            get { return WebConfigurationManager.ConnectionStrings["HTGConnectionString1"].ConnectionString; }
        }

        public List<Models.VenueOwner> GetGetVenueOwners()
        {
            var con = new SqlConnection(Connection);
            var cmd = new SqlCommand("EV_GetVenueOwners", con) { CommandType = CommandType.StoredProcedure };
            var owners = new List<Models.VenueOwner>();

            try
            {
                con.Open();
                SqlDataReader reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    var venueOwner = new VenueOwner((string)reader["EventfulID"], (string)reader["OwnerName"]);
                    owners.Add(venueOwner);
                }
                reader.Close();

                return owners;
            }

            catch (Exception)
            {
                throw new ApplicationException("Data Error");
            }
            finally
            {
                con.Close();
            }

        }
        
        public List<Brand> GetAutoCompleteBrand(string query)
        {
            var con = new SqlConnection(Connection);
            var cmd = new SqlCommand("GetAutoCompleteBrand", con) { CommandType = CommandType.StoredProcedure };
            cmd.Parameters.Add(new SqlParameter("@Query", SqlDbType.NVarChar, 50));
            cmd.Parameters["@Query"].Value = query;

            var brands = new List<Brand>();

            try
            {
                con.Open();
                SqlDataReader reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    var brand = new Brand((int)reader["BrandID"], (string)reader["BrandName"]);
                    brands.Add(brand);
                }
                reader.Close();
                return brands;
            }

            catch (Exception)
            {
                throw new ApplicationException("Data Error");
            }
            finally
            {
                con.Close();
            }
        }

        public List<Product> GetAutoCompleteProduct(string query)
        {
            var con = new SqlConnection(Connection);
            var cmd = new SqlCommand("GetAutoCompleteProduct", con) { CommandType = CommandType.StoredProcedure };
            cmd.Parameters.Add(new SqlParameter("@Query", SqlDbType.NVarChar, 50));
            cmd.Parameters["@Query"].Value = query;

            var dtBiz = new DataTable();
            // ReSharper disable AssignNullToNotNullAttribute
            dtBiz.Columns.Add("ProductID", Type.GetType("System.Int32"));
            dtBiz.Columns.Add("ProductName", Type.GetType("System.String"));
            // ReSharper restore AssignNullToNotNullAttribute

            var products = new List<Product>();

            try
            {
                con.Open();
                SqlDataReader reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    var product = new Product((int)reader["ProductID"], (string)reader["ProductName"]);
                    products.Add(product);
                }
                reader.Close();
                return products;
            }
            catch (Exception)
            {
                throw new ApplicationException("Data Error");
            }
            finally
            {
                con.Close();
            }
        }

        public List<Branch> GetAutoCompleteStore(string query)
        {
            var con = new SqlConnection(Connection);
            var cmd = new SqlCommand("GetAutoCompleteStore", con) { CommandType = CommandType.StoredProcedure };
            cmd.Parameters.Add(new SqlParameter("@Query", SqlDbType.NVarChar, 50));
            cmd.Parameters["@Query"].Value = query;

            var branches = new List<Branch>();

            try
            {
                con.Open();
                SqlDataReader reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    var branch = new Branch((int)reader["BranchID"], (string)reader["BusinessName"],
                                            (string)reader["ZoneName"]);
                    branches.Add(branch);
                }
                reader.Close();
                return branches;
            }
            catch (Exception)
            {
                throw new ApplicationException("Data Error");
            }
            finally
            {
                con.Close();
            }
        }

        public List<Venue> GetAllVenues()
        {
            var con = new SqlConnection(Connection);
            var cmd = new SqlCommand("EV_GetAllVenues", con) { CommandType = CommandType.StoredProcedure };

            var venues = new List<Venue>();

            try
            {
                con.Open();
                SqlDataReader reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    var venue = new Venue();

                    if (reader["VenueID"] != DBNull.Value)
                    {
                        venue.VenueId = Convert.ToInt32(reader["VenueID"]);
                    }

                    if (reader["Branch"] != DBNull.Value)
                    {
                        venue.BranchID = Convert.ToInt32(reader["Branch"]);
                    }

                    if (reader["VenueName"] != DBNull.Value)
                    {
                        venue.Name = (string)reader["VenueName"];
                    }

                    if (reader["EventfulID"] != DBNull.Value)
                    {
                        venue.EventfulId = (string)reader["EventfulID"];
                    }

                    if (reader["BranchImageURL"] != DBNull.Value)
                    {
                        venue.BranchImageURL = (string)reader["BranchImageURL"];
                    }

                    if (reader["BusinessName"] != DBNull.Value)
                    {
                        venue.BusinessName = (string)reader["BusinessName"];
                    }

                    if (reader["Affiliate"] != DBNull.Value)
                    {
                        venue.Affiliate = (bool)reader["Affiliate"];
                    }

                    venues.Add(venue);
                }
                reader.Close();
                return venues;
            }
            catch (Exception e)
            {
                throw new ApplicationException(e.Message);
            }
            finally
            {
                con.Close();
            }
        }
    }
}