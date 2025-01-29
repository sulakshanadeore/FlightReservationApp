using Microsoft.Data.SqlClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AirlinesLibrary
{
    public class PassengerDAL
    {

        public List<int> GetAllSeatsBooked(int flightno)
        {
            SqlCommand cmd = null;
            SqlConnection cn = null;
            List<int> list = new List<int>();
            try
            {
                cn = ConnectToDB();
                string sql = "select seatno from passenger where flightno=@fno";
                cmd = new SqlCommand(sql, cn);
                cmd.Parameters.AddWithValue("@fno", flightno);
                cn.Open();
                SqlDataReader dr = cmd.ExecuteReader();
                if (dr.HasRows)
                {
                    while (dr.Read())
                    {
                        list.Add(Convert.ToInt32(dr[0]));
                    }
                }
                else
                {
                    throw new InvalidFlightException("check the flightno u entered...");
                }

            }
            catch (InvalidFlightException ex) { throw ex; }
            catch (Exception ex)
            {
                throw ex;
            }
            finally { cmd.Dispose(); cn.Close(); cn.Dispose(); }
            return list;
            }

        public Passenger ViewTicket(int seatnumber, int flightnumber)
        {
            SqlCommand cmd = null;
            SqlConnection cn = null;
            Passenger p=new Passenger();
            try
            {
                cn = ConnectToDB();
                string sql = "select * from passenger where seatno=@sno and flightno=@fno";
                cmd = new SqlCommand(sql, cn);
                cmd.Parameters.AddWithValue("@sno", seatnumber);
                cmd.Parameters.AddWithValue("@fno", flightnumber);
                cn.Open();
                SqlDataReader dr = cmd.ExecuteReader();
                
                if (dr.HasRows)
                {
                    dr.Read();
                    
                        p.SeatNo = seatnumber;
                        p.FlightNo = flightnumber;
                        p.SeatType = dr[1].ToString();
                        //p.SeatType = dr["SeatType"].ToString();
                        p.Destination = dr["Destination"].ToString();
                        p.StartingPoint = dr["StartingPoint"].ToString();
                    
                    
                }
                else
                {
                    throw new PassengerDetailsNotFoundException("Such booking doesn't exists....");
                }
            }
            catch (PassengerDetailsNotFoundException ex)
            {
                throw ex;
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                cmd.Dispose();
                cn.Close();
                cn.Dispose();
            }
            return p;   
        
        
        }

        public bool DeleteBooking(int seatnumber, int flightnumber)
        {
            SqlCommand cmd = null;
            bool txnStatus = false;
            SqlConnection cn = null;
            try
            {
                 cn = ConnectToDB();
                string sql = "delete from passenger where seatno=@sno and flightno=@fno";
                cmd = new SqlCommand(sql, cn);
                cmd.Parameters.AddWithValue("@sno", seatnumber);
                cmd.Parameters.AddWithValue("@fno", flightnumber);
                cn.Open();
                cmd.ExecuteNonQuery();
                txnStatus = true;
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                cmd.Dispose();
                cn.Close();
                cn.Dispose();

            }


            return txnStatus;




        }
        public bool InsertBooking(Passenger passenger) 
        {
            SqlConnection cn=null; SqlCommand cmd = null; bool txnstatus = false;
            try
            {
                cn = ConnectToDB();
                string sql = "insert into Passenger values(@seatno,@seattype,@flightno,@start,@dest)";
                cmd = new SqlCommand(sql, cn);
                cmd.Parameters.AddWithValue("@seatno", passenger.SeatNo);
                cmd.Parameters.AddWithValue("@seattype", passenger.SeatType);
                cmd.Parameters.AddWithValue("@flightno", passenger.FlightNo);
                cmd.Parameters.AddWithValue("@start", passenger.StartingPoint);
                cmd.Parameters.AddWithValue("@dest", passenger.Destination);
                cn.Open();
                cmd.ExecuteNonQuery();
                txnstatus = true;
            }
            catch (Exception ex) {
                throw ex;
            }
            finally {
                cmd.Dispose();
                cn.Close();
                cn.Dispose();
                    }
            return txnstatus;
        
        }

        private static SqlConnection ConnectToDB()
        {
            SqlConnection cn;
            string cnstring = "server=.\\sqlexpress;Integrated Security=true;Trust Server Certificate=true;database=IndigoAirlines";
            cn = new SqlConnection(cnstring);
            return cn;
        }
    }
}
