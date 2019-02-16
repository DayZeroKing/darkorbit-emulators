﻿using Ow.Game.Objects;
using Ow.Game.Objects.Players.Managers;
using Ow.Net.netty;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ow.Game.Objects.Players.Techs
{
    class PrecisionTargeter
    {
        public Player Player { get; set; }

        public bool Active = false;

        public PrecisionTargeter(Player player)
        {
            Player = player;
        }

        public void Tick()
        {
            if (Active)
                if (cooldown.AddMilliseconds(TimeManager.PRECISION_TARGETER_DURATION) < DateTime.Now)
                    Disable();
        }

        public DateTime cooldown = new DateTime();
        public void Send()
        {
            if (cooldown.AddMilliseconds(TimeManager.PRECISION_TARGETER_DURATION + TimeManager.PRECISION_TARGETER_COOLDOWN) < DateTime.Now || Player.GodMode)
            {
                Player.PrecisionTargeter = true;

                Active = true;
                cooldown = DateTime.Now;
            }
        }

        public void Disable()
        {
            Active = false;
            Player.PrecisionTargeter = false;
            Player.TechManager.SendTechStatus();
            Player.SendCooldown(ServerCommands.TECH_ROCKET_PROBABILITY_MAXIMIZER, TimeManager.PRECISION_TARGETER_COOLDOWN);
        }
    }
}