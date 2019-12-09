using System;
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
        //bool reefer;               //(false)0 for dry, (true)1 for refreigerated
        int BillDays;
        int distance;

        
        /// <summary>
        /// The trip class maintains the calculations for figuring out how long and far a delivery will take
        /// </summary>
        /// <param name="heading"></param>
        public Trip(int Quantity, int dest, int originCity, City[] CityList)
        {
            workTime = 0;
            driveToday = 0;
            destination = dest;                  //destination city
            currentCity = originCity;           //change to next city
            if (destination > currentCity)      //Headed east
            {
                CurrentDrive = CityList[currentCity].EastMinutes;
                direction = 1;
            }
            else
            {
                direction = -1;
                CurrentDrive = CityList[currentCity - 1].EastMinutes;
            }
            currentCity += direction;
            daytotal = -1;
            quantity = Quantity;        //0 = FTL >0 = # of pallets
            unloading = LoadTime;       //truck has to be loaded before it can leave

            distance = CurrentDrive;
            while (currentCity != destination)
            {
                if (direction > 0)      //Headed east
                {
                    distance += CityList[currentCity].EastMinutes;
                }
                else
                {
                    distance += CityList[currentCity - 1].EastMinutes;
                }
                currentCity += direction;
            }
            currentCity = originCity + direction;
            while (direction != 0)      //Get bill data on creation for estimates
            {
                this.AddTime(CityList);
            }
            BillDays = daytotal;

            workTime = 0;
            driveToday = 0;
            destination = dest;                  //destination city
            currentCity = originCity;           //change to next city
            if (destination > currentCity)      //Headed east
            {
                CurrentDrive = CityList[currentCity].EastMinutes;
                direction = 1;
            }
            else
            {
                direction = -1;
                CurrentDrive = CityList[currentCity - 1].EastMinutes;
            }
            currentCity += direction;
            daytotal = -1;
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
                        workTime += addMinutes;
                        addMinutes = 0;
                    }
                    else if (((CurrentDrive + driveToday) <= DrivingMax) && ((CurrentDrive + workTime) <= MaxHours))// this is what is expected to run
                    {
                        driveToday += CurrentDrive; //update drive time for day
                        workTime += CurrentDrive;
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
                    else if ((CurrentDrive + driveToday - DrivingMax) <= (CurrentDrive + workTime - MaxHours))//figure out the limiter, DrivingMax or Max Hours
                    {
                        //driving time is the limiting factor, or at least equal with workTime
                        CurrentDrive = CurrentDrive - (DrivingMax - driveToday);
                        addMinutes = 0;
                        driveToday = DrivingMax;
                    }
                    else //cannot finish the drive, not enough worktime/Maxhours
                    {
                        CurrentDrive = CurrentDrive - (MaxHours - workTime);
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
