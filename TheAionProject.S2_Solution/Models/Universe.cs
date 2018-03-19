using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TheAionProject
{
    /// <summary>
    /// class of the game map
    /// </summary>
    public class Universe
    {
        #region ***** define all lists to be maintained by the Universe object *****

        //
        // list of all space-time locations
        //
        private List<SpaceTimeLocation> _spaceTimeLocations;

        public List<SpaceTimeLocation> SpaceTimeLocations
        {
            get { return _spaceTimeLocations; }
            set { _spaceTimeLocations = value; }
        }

        #endregion

        #region ***** constructor *****

        //
        // default Universe constructor
        //
        public Universe()
        {
            //
            // add all of the universe objects to the game
            // 
            IntializeUniverse();
        }

        #endregion

        #region ***** define methods to initialize all game elements *****

        /// <summary>
        /// initialize the universe with all of the space-time locations
        /// </summary>
        private void IntializeUniverse()
        {
            _spaceTimeLocations = UniverseObjects.SpaceTimeLocations;
        }

        #endregion

        #region ***** define methods to return game element objects and information *****
        
        public bool IsValidSpaceTimeLocationId(int spaceTimeLocationId)
        {
            List<int> spaceTimeLocationIds = new List<int>();

            //
            // create a list of space-time location ids
            //
            foreach (SpaceTimeLocation stl in _spaceTimeLocations)
            {
                spaceTimeLocationIds.Add(stl.SpaceTimeLocationID);
            }

            //
            // determine if the space-time location id is a valid id and return the result
            //
            if (spaceTimeLocationIds.Contains(spaceTimeLocationId))
            {
                return true;
            }
            else
            {
                return false;
            }
        }


        /// <summary>
        /// determine if a location is accessible to the player
        /// </summary>
        /// <param name="spaceTimeLocationId"></param>
        /// <returns>accessible</returns>
        public bool IsAccessibleLocation(int spaceTimeLocationId)
        {
            SpaceTimeLocation spaceTimeLocation = GetSpaceTimeLocationById(spaceTimeLocationId);
            if (spaceTimeLocation.Accessible == true)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        /// <summary>
        /// return the next available ID for a SpaceTimeLocation object
        /// </summary>
        /// <returns>next SpaceTimeLocationObjectID </returns>
        public int GetMaxSpaceTimeLocationId()
        {
            int MaxId = 0;

            foreach (SpaceTimeLocation spaceTimeLocation in SpaceTimeLocations)
            {
                if (spaceTimeLocation.SpaceTimeLocationID > MaxId)
                {
                    MaxId = spaceTimeLocation.SpaceTimeLocationID;
                }
            }

            return MaxId;
        }

        /// <summary>
        /// get a SpaceTimeLocation object using an Id
        /// </summary>
        /// <param name="Id">space-time location Id</param>
        /// <returns>requested space-time location</returns>
        public SpaceTimeLocation GetSpaceTimeLocationById(int Id)
        {
            SpaceTimeLocation spaceTimeLocation = null;

            //
            // run through the space-time location list and grab the correct one
            //
            foreach (SpaceTimeLocation location in _spaceTimeLocations)
            {
                if (location.SpaceTimeLocationID == Id)
                {
                    spaceTimeLocation = location;
                }
            }

            //
            // the specified ID was not found in the universe
            // throw and exception
            //
            if (spaceTimeLocation == null)
            {
                string feedbackMessage = $"The Space-Time Location ID {Id} does not exist in the current Universe.";
                throw new ArgumentException(Id.ToString(), feedbackMessage);
            }

            return spaceTimeLocation;
        }

        public void UpdateLocation(Player currentLocation)
        {
            // For Start room / Room ID 1
            if (currentLocation.SpaceTimeLocationID == 1)
            {
                foreach (SpaceTimeLocation l in SpaceTimeLocations)
                {
                    if (l.SpaceTimeLocationID == 2)
                    {
                        l.Accessible = true;
                    }
                    else
                    {
                        l.Accessible = false;
                    }
                }
            }

            // For Room ID 2
            else if (currentLocation.SpaceTimeLocationID == 2)
            {
                foreach (SpaceTimeLocation l in SpaceTimeLocations)
                {
                    if (l.SpaceTimeLocationID == 1 || l.SpaceTimeLocationID == 3 || l.SpaceTimeLocationID == 5)
                    {
                        l.Accessible = true;
                    }
                    else
                    {
                        l.Accessible = false;
                    }
                }
            }

            // For Room ID 3
            else if (currentLocation.SpaceTimeLocationID == 3)
            {
                foreach (SpaceTimeLocation l in SpaceTimeLocations)
                {
                    if (l.SpaceTimeLocationID == 2 || l.SpaceTimeLocationID == 4)
                    {
                        l.Accessible = true;
                    }
                    else
                    {
                        l.Accessible = false;
                    }
                }
            }

            // For Room ID 4
            else if (currentLocation.SpaceTimeLocationID == 4)
            {
                foreach (SpaceTimeLocation l in SpaceTimeLocations)
                {
                    if (l.SpaceTimeLocationID == 3)
                    {
                        l.Accessible = true;
                    }
                    else
                    {
                        l.Accessible = false;
                    }
                }
            }

            // For Room ID 5
            else if (currentLocation.SpaceTimeLocationID == 5)
            {
                foreach (SpaceTimeLocation l in SpaceTimeLocations)
                {
                    if (l.SpaceTimeLocationID == 2 || l.SpaceTimeLocationID == 6)
                    {
                        l.Accessible = true;
                    }
                    else
                    {
                        l.Accessible = false;
                    }
                }
            }

            // For Room ID 6
            else if (currentLocation.SpaceTimeLocationID == 6)
            {
                foreach (SpaceTimeLocation l in SpaceTimeLocations)
                {
                    if (l.SpaceTimeLocationID == 11 || l.SpaceTimeLocationID == 5 || l.SpaceTimeLocationID == 7)
                    {
                        l.Accessible = true;
                    }
                    else
                    {
                        l.Accessible = false;
                    }
                }
            }

            // For Room ID 7
            else if (currentLocation.SpaceTimeLocationID == 7)
            {
                foreach (SpaceTimeLocation l in SpaceTimeLocations)
                {
                    if (l.SpaceTimeLocationID == 6 || l.SpaceTimeLocationID == 8 || l.SpaceTimeLocationID == 10)
                    {
                        l.Accessible = true;
                    }
                    else
                    {
                        l.Accessible = false;
                    }
                }
            }

            // For Room ID 8
            else if (currentLocation.SpaceTimeLocationID == 8)
            {
                foreach (SpaceTimeLocation l in SpaceTimeLocations)
                {
                    if (l.SpaceTimeLocationID == 7 || l.SpaceTimeLocationID == 9)
                    {
                        l.Accessible = true;
                    }
                    else
                    {
                        l.Accessible = false;
                    }
                }
            }

            // For Room ID 9
            else if (currentLocation.SpaceTimeLocationID == 9)
            {
                foreach (SpaceTimeLocation l in SpaceTimeLocations)
                {
                    if (l.SpaceTimeLocationID == 8)
                    {
                        l.Accessible = true;
                    }
                    else
                    {
                        l.Accessible = false;
                    }
                }
            }

            // For Room ID 10
            else if (currentLocation.SpaceTimeLocationID == 10)
            {
                foreach (SpaceTimeLocation l in SpaceTimeLocations)
                {
                    if (l.SpaceTimeLocationID == 7)
                    {
                        l.Accessible = true;
                    }
                    else
                    {
                        l.Accessible = false;
                    }
                }
            }

            // For Room ID 11
            else if (currentLocation.SpaceTimeLocationID == 11)
            {
                foreach (SpaceTimeLocation l in SpaceTimeLocations)
                {
                    if (l.SpaceTimeLocationID == 6 || l.SpaceTimeLocationID == 12)
                    {
                        l.Accessible = true;
                    }
                    else
                    {
                        l.Accessible = false;
                    }
                }
            }

            // FOR ROOM ID 13
            else if (currentLocation.SpaceTimeLocationID == 13)
            {
                foreach (SpaceTimeLocation l in SpaceTimeLocations)
                {
                    if (l.SpaceTimeLocationID == 12 || l.SpaceTimeLocationID == 14)
                    {
                        l.Accessible = true;
                    }
                    else
                    {
                        l.Accessible = false;
                    }
                }
            }
        }

        #endregion
    }
}
