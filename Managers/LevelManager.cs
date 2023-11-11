﻿using SprintZero1.LevelFiles;
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
        /* for mouse commands */
        private static int currentRoomIndex = 0;
        /// <summary>
        /// Get the room list
        /// </summary>
        public static List<string> DungeonRoomList { get { return _dungeonRoomMap.Keys.ToList(); } }
        /// <summary>
        /// Get the current room index and set the current index
        /// </summary>
        public static int CurrentRoomIndex { get { return currentRoomIndex; } set { currentRoomIndex = value; } }
        /// <summary>
        /// Initializes the level manager
        /// </summary>
        public static void Initialize()
        {
            LevelXMLParser parser = new LevelXMLParser();
            string LevelFolderPath = @"XMLFiles/LevelXmlFiles";
            foreach (var filePath in Directory.EnumerateFiles(LevelFolderPath))
            {
                DungeonRoom room = parser.Parse(filePath);
                _dungeonRoomMap.Add(room.RoomName, room);
            }
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
            return _dungeonRoomMap[roomName];
        }

    }
}
