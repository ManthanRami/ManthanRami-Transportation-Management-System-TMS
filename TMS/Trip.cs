﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TMS
{
    /// <summary>
    /// Maintains information relevant to the orders in order to properly calculate the bill for the customers
    /// </summary>
    public class Trip
    {
        static int MaxHours = 720;  //12 hours in minutes
        static int DrivingMax = 480;//8 hours in minutes
        static int LoadTime = 120;  //two hours to load/unload
        int destination;         //final city
        int currentCity;         //enroute to next city
        int workTime;               //minutes working today
        int driveToday;             //minutes driving today
        int CurrentDrive;           //minute to next city
        int direction;              //0 = last stop. 1 = east -1 = west
        int quantity;               //0 = FTL >0 = # of pallets
        int daytotal;              //days for the trip
        int unloading;              //track unloading time left
        bool reefer;               //(false)0 for dry, (true)1 for refreigerated
        
        /// <summary>
        /// The trip class maintains the calculations for figuring out how long and far a delivery will take
        /// </summary>
        /// <param name="heading"></param>
        public Trip(int Quantity, int dest, int originCity, City[] CityList)
        {
            workTime = 0;
            driveToday = 0;
            destination = dest;         //destination city
            currentCity = originCity;   //change to next city
            if (destination > currentCity)      //Headed east
            {
                direction = 1;
            }
            else
            {
                direction = -1;
            }
            daytotal = -1;
            CurrentDrive = 0;           //get time to next city
            quantity = Quantity;        //0 = FTL >0 = # of pallets
            unloading = LoadTime;       //truck has to be loaded before it can leave
        }

        /// <summary>
        /// Allows the planner role to simulate the passage of time
        /// </summary>
        /// <param name="AddHours">Number of hours as a float, to add to the current trip</param>
        /// <returns>0 for normal running. 1 is complete</returns>
        //public int AddTime(float AddHours = 24)
        public int AddTime(City[] CityList)
        {
            int addMinutes = 1440; //minutes in 1 day, the planner time advancement interval
            //Time left in day, time left in driving limit, time left in working limit
            while (addMinutes > 0 && (driveToday < DrivingMax || workTime < MaxHours))
            {
                if (unloading > 0)//load or unload the truck
                {
                    if (unloading <= addMinutes) 
                    {
                        if ((workTime + unloading) <= MaxHours)
                        {
                            workTime += unloading;
                            addMinutes -= unloading;
                            unloading = 0;
                            //finished unloading truck.
                            if (destination == currentCity)//Truck arived at destination
                            {
                                direction = 0;
                                addMinutes = 0;
                            }
                            else //Truck must drive on (should never trigger for FTL)
                            {
                                if (direction == 1)//LTL moving to next city east
                                {
                                    CurrentDrive = CityList[currentCity].EastMinutes;
                                    currentCity++;
                                }
                                else 
                                {
                                    currentCity--;
                                    CurrentDrive = CityList[currentCity].EastMinutes;
                                }
                            }
                        }
                        else //not enough time to finish unloading truck today. Wait for tomorrow
                        {
                            addMinutes = 0;
                            unloading = unloading + workTime - MaxHours;
                        }
                    }
                    //not enough given time to finish unload. not going to happen with full day time increments
                    else
                    {
                        unloading -= addMinutes;
                        addMinutes = 0;
                    }
                }
                else //not loading, driving to next city
                {
                    if (CurrentDrive >= addMinutes && (CurrentDrive + driveToday) <= DrivingMax)//should never trigger with full day increments
                    {
                        if (CurrentDrive == addMinutes)//arrived
                        {
                            //Get next city
                        }
                        CurrentDrive -= addMinutes;
                        addMinutes = 0;
                    }
                    else if (((CurrentDrive + driveToday) <= DrivingMax) && ((CurrentDrive + workTime) <= MaxHours))// this is what is expected to run
                    {
                        driveToday += CurrentDrive; //update drive time for day
                        addMinutes -= CurrentDrive;
                        CurrentDrive = 0;
                        if (quantity == 0)//FTL
                        {
                            if (currentCity == destination)
                            {
                                unloading = LoadTime;
                            }
                        }
                        else //LTL
                        {
                            unloading = LoadTime;
                        }
                    }
                    else if() //cannot finish the drive
                    {
                        CurrentDrive = CurrentDrive + driveToday - DrivingMax;
                        addMinutes = 0;
                    }
                }
            }
            //Add a day to trip, reset work hours and drive hours for a new workday
            daytotal++;
            workTime = 0;
            driveToday = 0;
            return direction;
        }
    }

}
