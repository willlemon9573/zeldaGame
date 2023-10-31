﻿using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Media;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SprintZero1.Factories
{
    public class SoundFactory
    {

        private Dictionary<string, Song> backgroundMusic = new Dictionary<string, Song>(); //background music
        //Dictionary for all the sound effects
        private Dictionary<string, SoundEffect> soundEffects = new Dictionary<string, SoundEffect>();

        /// <summary>
        /// SoundFactory, load all the sound effects and create sound effects dictionary
        /// </summary>
        /// <param name="content">Content Manager</param>
        public SoundFactory(ContentManager content)
        {
            //Background music
            backgroundMusic.Add("Dungeon_Music", content.Load<Song>("DungeonMusic"));
            //Add sound effects to dictionary
            soundEffects.Add("Bomb", content.Load<SoundEffect>("LOZ_Bomb_Blow"));
            soundEffects.Add("Boss_Scream", content.Load<SoundEffect>("LOZ_Boss_Scream1"));
            soundEffects.Add("Candle", content.Load<SoundEffect>("LOZ_Candle"));
            soundEffects.Add("Door_Unlock", content.Load<SoundEffect>("LOZ_Door_Unlock"));
            soundEffects.Add("Fanfare", content.Load<SoundEffect>("LOZ_Fanfare"));
            soundEffects.Add("Heart", content.Load<SoundEffect>("LOZ_Get_Heart"));
            soundEffects.Add("Item", content.Load<SoundEffect>("LOZ_Get_Item"));
            soundEffects.Add("Link_Die", content.Load<SoundEffect>("LOZ_Link_Die"));
            soundEffects.Add("Link_Hurt", content.Load<SoundEffect>("LOZ_Link_Hurt"));
            soundEffects.Add("Low_health", content.Load<SoundEffect>("LOZ_LowHealth"));
            soundEffects.Add("Magical_Rod", content.Load<SoundEffect>("LOZ_MagicalRod"));
            soundEffects.Add("Recorder", content.Load<SoundEffect>("LOZ_Recorder"));
            soundEffects.Add("Secret", content.Load<SoundEffect>("LOZ_Secret"));
            soundEffects.Add("Shield", content.Load<SoundEffect>("LOZ_Shield"));
            soundEffects.Add("Shore", content.Load<SoundEffect>("LOZ_Shore"));
            soundEffects.Add("Stairs", content.Load<SoundEffect>("LOZ_Stairs"));
            soundEffects.Add("Sword", content.Load<SoundEffect>("LOZ_Sword_Combined"));
        }

        /// <summary>
        /// Play sound given sound effect name
        /// </summary>
        /// <param name="name">Sound effect name</param>
        public void PlaySound(SoundEffect soundEffect)
        {
            soundEffect.Play();
        }
        
        /// <summary>
        /// Get sound effect via sound effect name
        /// </summary>
        /// <param name="name">Name of sound effect</param>
        /// <returns>Sound effect</returns>
        public SoundEffect GetSound(string name)
        {
            return soundEffects[name];
        }

        /// <summary>
        /// Play the background music and keep it repeating given a song
        /// </summary>
        public void PlayMusic(Song music)
        {
            MediaPlayer.Play(music);
            MediaPlayer.IsRepeating = true;
        }

        /// <summary>
        /// Get music
        /// </summary>
        /// <param name="name">name of music</param>
        /// <returns>music song</returns>
        public Song GetMusic(string name)
        {
            return backgroundMusic[name];
        }

       
        /// <summary>
        /// Stop background music
        /// </summary>
        public void StopMusic()
        {
            MediaPlayer.Stop();
        }

        /// <summary>
        /// Adjust volume of sound effects
        /// </summary>
        /// <param name="volume">New volume value</param>
        public void AdjustSoundEffectsVolume(float volume)
        {
            //Note: max volume is 1.0, lowest volume is 0.0
            SoundEffect.MasterVolume = volume;
        }

        /// <summary>
        /// Adjust volume of music
        /// </summary>
        /// <param name="volume">New volume value</param>
        public void AdjustMusicVolume(float volume)
        {
            //Note: max volume is 1.0, lowest volume is 0.0
            MediaPlayer.Volume = volume;
        }

    }
}
