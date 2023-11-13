using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Media;
using SprintZero1.XMLParsers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

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
            string path = @"XMLFiles\Sound.xml";
            XDocument document = XDocument.Load(path);
            XElement root = document.Root;
            XDocTools xDocTools = new XDocTools();

            foreach (XElement sound_effect in root.Elements("Sound"))
            {
                /* Get the sound effect name */
                string name = xDocTools.ParseAttributeAsString(sound_effect.Attribute("name"));
                XElement soundEffect = sound_effect.Element("SoundEffect");
                string soundWavName = xDocTools.ParseAttributeAsString(soundEffect.Attribute("sound"));
                SoundEffect sound = content.Load<SoundEffect>(soundWavName);
                soundEffects.Add(name, sound);
            }

            foreach (XElement song in root.Elements("Song"))
            {
                string name = xDocTools.ParseAttributeAsString(song.Attribute("name"));
                XElement background_music = song.Element("SoundEffect");
                string songMP3 = xDocTools.ParseAttributeAsString(background_music.Attribute("song"));
                Song songLoad = content.Load<Song>(songMP3);
                backgroundMusic.Add(name, songLoad);
            }

        }

        public void Initialize(ContentManager content)
        {
            
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
