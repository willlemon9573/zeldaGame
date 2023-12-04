using SprintZero1.Enums;
using SprintZero1.LevelFiles;
using SprintZero1.XMLParsers;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;

namespace SprintZero1.Managers
{
    internal static class LevelManager
    {
        /// <summary>
        /// Holds all the dungeon room information
        /// </summary>
        private static readonly Dictionary<string, DungeonRoom> _dungeonRoomMap = new Dictionary<string, DungeonRoom>();

        private static Dictionary<string, bool> _visitedRooms = new Dictionary<string, bool>();
        public static Dictionary<string, bool> WhetherVisitedRoom { get { return _visitedRooms; } }
        /* for mouse commands */
        private static int currentRoomIndex = 0;
        /// <summary>
        /// Get the room list
        /// </summary>
        public static List<string> DungeonRoomList { get { return _dungeonRoomMap.Keys.ToList(); } }
        private static string _playerCurrentRoom;
        public static string PlayerCurrentRoom { get { return _playerCurrentRoom; } }
        /// <summary>
        /// Get the current room index and set the current index
        /// </summary>
        public static int CurrentRoomIndex { get { return currentRoomIndex; } set { currentRoomIndex = value; } }

        public static void Load()
        {
            LevelXMLParser parser = new LevelXMLParser();
            string LevelFolderPath = @"XMLFiles/LevelXmlFiles";
            foreach (var filePath in Directory.EnumerateFiles(LevelFolderPath))
            {
                DungeonRoom room = parser.Parse(filePath);
                _visitedRooms.Add(room.RoomName, false);
                _dungeonRoomMap.Add(room.RoomName, room);
            }
        }


        public static Dictionary<string, bool> GetWhetherVisitedRoom()
        {
            return _visitedRooms;
        }

        /// <summary>
        /// Returns the desired dungeon room that contains all the information about that room
        /// </summary>
        /// <param name="roomName">the name of the room to use</param>
        /// <returns>the instance of the dungeon room with its current data</returns>
        public static DungeonRoom GetDungeonRoom(string roomName)
        {
            Debug.Assert(roomName != null, "roomName cannot be null");
            Debug.Assert(_dungeonRoomMap.ContainsKey(roomName));
            _visitedRooms[roomName] = true;
            _playerCurrentRoom = roomName;
            return _dungeonRoomMap[roomName];
        }

        /// <summary>
        /// Unlocks the given door in the room specified
        /// </summary>
        /// <param name="roomToFind">The room that has the door that needs opened</param>
        /// <param name="doorDirection">The direction which the door is placed</param>
        public static void OpenDoor(string roomToFind, Direction doorDirection)
        {
            if (_dungeonRoomMap.TryGetValue(roomToFind, out DungeonRoom room))
            {
                room.UnlockDoor(doorDirection);
            }
        }

        public static void Reset()
        {
            _dungeonRoomMap.Clear();
            _visitedRooms.Clear();
        }
    }
}
