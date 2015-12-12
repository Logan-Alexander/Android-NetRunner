using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace NetRunner.UI.Xna.Components
{
    public enum NetRunnerSound
    {
        Boop
    }

    public interface INetRunnerSoundManager
    {
        void Play(NetRunnerSound sound);
    }
    
    public class NetRunnerSoundManager : DrawableGameComponent, INetRunnerSoundManager
    {
        private SoundEffect _Boop;
        private Dictionary<NetRunnerSound, SoundEffect> _SoundEffectLookup;

        public NetRunnerSoundManager(Game game)
            : base(game)
        {
            game.Components.Add(this);
            game.Services.AddService(typeof(INetRunnerSoundManager), this);
        }

        protected override void LoadContent()
        {
            _SoundEffectLookup = new Dictionary<NetRunnerSound, SoundEffect>();

            _Boop = Game.Content.Load<SoundEffect>("SoundEffects/Boop");
            _SoundEffectLookup.Add(NetRunnerSound.Boop, _Boop);
        }

        public void Play(NetRunnerSound sound)
        {
            SoundEffect soundEffect = _SoundEffectLookup[sound];

            soundEffect.Play();
        }
    }
}
