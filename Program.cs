using AirlinesLibrary;

internal class Program
{
    private static void Main(string[] args)
    {
        Console.WriteLine("Menu \n 1. Book Flight  \n 2. Cancel Booking \n 3. View Booking \n 4. booked Tickets By Flight \n 5. Exit");
        int ch = Convert.ToInt32(Console.ReadLine());
        PassengerUtility util = new PassengerUtility();
        switch (ch)
        {
            case 1:
                BookingOfTicket(util);
                break;
            case 2:
                CancelBooking(util);
                break;
            case 3:
                PrintTicket(util);

                break;
            case 4:
                ShowSeatsBooked(util);
                break;
            case 5:
                Environment.Exit(1);
                break;
            default:
                break;
        }

       
    }

    private static void ShowSeatsBooked(PassengerUtility util)
    {
        try
        {
            Console.WriteLine("Enter Flight no:");
            util.FlightNo = Convert.ToInt32(Console.ReadLine());
            List<int> seatsBooked = util.ShowTicketsBooked(util.FlightNo);
            Console.WriteLine("Seats Booked: ");
            foreach (var item in seatsBooked)
            {
                Console.WriteLine(item);
            }
        }
        catch (InvalidFlightException ex)
        {

            Console.WriteLine(ex.Message);
        }
        catch (Exception ex) {
            Console.WriteLine(ex.Message);
        
        }
    }

    private static void PrintTicket(PassengerUtility util)
    {

        try
        {
            Console.WriteLine("Enter Seatno:");
            util.SeatNo = Convert.ToInt32(Console.ReadLine());
            Console.WriteLine("Enter Flight no:");
            util.FlightNo = Convert.ToInt32(Console.ReadLine());
            Passenger passengerData = util.ShowTicket(sno: util.SeatNo, fno: util.FlightNo);
            Console.WriteLine("Following details were found: ");
            Console.WriteLine($"Seat No= {passengerData.SeatNo}");
            Console.WriteLine($"Flightno= {passengerData.FlightNo}");
            Console.WriteLine($"Starting Point= {passengerData.StartingPoint}");
            Console.WriteLine($"Destination= {passengerData.Destination}");
            Console.WriteLine("SeatType: " + passengerData.SeatType);
        }
        catch (PassengerDetailsNotFoundException ex)
        {
            Console.WriteLine(ex.Message);
        }
        catch (Exception ex)
        {

            Console.WriteLine(ex.Message);
        }
    }

    private static void CancelBooking(PassengerUtility util)
    {
        Console.WriteLine("Enter Seatno:");
        util.SeatNo = Convert.ToInt32(Console.ReadLine());
        Console.WriteLine("Enter Flight no:");
        util.FlightNo = Convert.ToInt32(Console.ReadLine());
        bool txnStatus = util.CancelBooking(util.SeatNo, util.FlightNo);
        if (txnStatus)
        {
            Console.WriteLine("Cancellation Successful....");
        }
        else
        {


            Console.WriteLine("Cancellation UnSuccessful....");
        }
    }

    private static void BookingOfTicket(PassengerUtility util)
    {
        try
        {

            Console.WriteLine("Enter Seat No");
            util.SeatNo = Convert.ToInt32(Console.ReadLine());
            Console.WriteLine("Enter Seat Type");
            util.SeatType = Console.ReadLine();
            Console.WriteLine("Enter Flight No");
            util.FlightNo = Convert.ToInt32(Console.ReadLine());
            Console.WriteLine("Enter Source of Flight");
            util.StartingPoint = Console.ReadLine();
            Console.WriteLine("Enter Destination");
            util.Destination = Console.ReadLine();
            try
            {

                bool txnstatus = util.BookTicket();
                if (txnstatus)
                {
                    Console.WriteLine("Booking Successful....");
                }
                else
                {


                    Console.WriteLine("Booking UnSuccessful....");
                }
            }
            catch (Exception ex)
            {

                Console.WriteLine(ex.Message);
            }


        }
        catch (Exception ex)
        {
            Console.WriteLine(ex.Message);

        }
    }
}