using System;

namespace FlightBooking.Console.Utilities
{
    public static class PassengerUtilities
    {

        /// <summary>
        /// Method to extract passenger name from a string
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        public static string GetPassengerName(this string value)
        {
            return value.Split(" ")[2];
        }


        /// <summary>
        /// Method to extract passenger age from a string
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        public static int GetPassengerAge(this string value)
        {
            return Convert.ToInt32(value.Split(" ")[3]);
        }

        /// <summary>
        /// Method to extract passenger loyalty point
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        public static int GetPassengerLoyaltyPoints(this string value)
        {
            return Convert.ToInt32(value.Split(" ")[4]);
        }

        /// <summary>
        /// Method to extract passenger loyalty point
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        public static bool IsUsingLoyaltyPoints(this string value)
        {
            return Convert.ToBoolean(value.Split(" ")[5]);
        }
    }
}
