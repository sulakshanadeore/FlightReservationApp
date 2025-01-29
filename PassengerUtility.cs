using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AirlinesLibrary
{
    public class PassengerUtility:Passenger
    {
        //PassengerDAL dal = new PassengerDAL();
        PassengerDAL dal;
       

        public PassengerUtility()
        {
                dal=new PassengerDAL();
        }

        public List<int> ShowTicketsBooked(int fno)
        {
            List<int> bookedSeats=new List<int>();
            try
            {
                bookedSeats = dal.GetAllSeatsBooked(fno);
            }
            catch (InvalidFlightException ex)
            {
                throw ex;
            }
            catch (Exception ex)
            {

                throw ex;
            }
            return bookedSeats;        
        }

        public Passenger ShowTicket(int sno, int fno)
        {
            Passenger data = null;
            try
            {
                 data = dal.ViewTicket(sno, fno);
            }
            catch (PassengerDetailsNotFoundException ex)
            {
                throw ex;
            }
            catch (Exception ex) 
            {
                throw ex;
            }
            return data;
        
        }
        public bool CancelBooking(int sno, int fno)
        {
              bool txnStatus;
            try
            {
               txnStatus= dal.DeleteBooking(sno, fno);
            }
            catch (Exception ex)
            {

                throw ex;
            }
        return txnStatus;
        }

        public bool BookTicket()
        {
            Passenger passenger=new Passenger();
            passenger.SeatNo=this.SeatNo;
            passenger.SeatType=this.SeatType;
            passenger.StartingPoint=this.StartingPoint;
            passenger.Destination=this.Destination;
            passenger.FlightNo=this.FlightNo;
            bool Txnstatus = false;
            try
            {
                 Txnstatus = dal.InsertBooking(passenger);
                return Txnstatus;
            }
            catch (Exception ex)
            {

                throw ex;
            }
       
            
        }


    }
}
