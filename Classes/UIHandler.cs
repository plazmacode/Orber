using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;
using System.Text;

namespace Orber
{
    public static class UIHandler
    {
        private static MouseState oldState;

        public static void Update(GameTime gameTime)
        {
            foreach (UIElement ui in GameWorld.UIListProp)
            {
                if (ui.Type == "button")
                {
                    //If left button clicked while UI contains mouse
                    if (oldState.LeftButton == ButtonState.Pressed &&
                        GameWorld.MouseStateProp.LeftButton == ButtonState.Released &&
                        ui.CollisionBoxProp.Contains(GameWorld.MouseStateProp.Position))
                    {
                        switch (ui.Name)
                        {
                            case "Make White Orbs":
                                if (OrbSystem.Orbs[0] >= 10)
                                {
                                    OrbSystem.Orbs[0] -= 10;
                                    OrbSystem.Orbs[1] += 1;
                                }
                                break;
                            case "Make Blue Orbs":
                                if (OrbSystem.Orbs[1] >= 10)
                                {
                                    OrbSystem.Orbs[1] -= 10;
                                    OrbSystem.Orbs[2] += 1;
                                }
                                break;
                            case "Make Yellow Orbs":
                                if (OrbSystem.Orbs[2] >= 10)
                                {
                                    OrbSystem.Orbs[2] -= 10;
                                    OrbSystem.Orbs[3] += 1;
                                }
                                break;
                            case "Make Orange Orbs":
                                if (OrbSystem.Orbs[3] >= 10)
                                {
                                    OrbSystem.Orbs[3] -= 10;
                                    OrbSystem.Orbs[4] += 1;
                                }
                                break;
                            default:
                                break;
                        }
                    }
                }
            }
            oldState = GameWorld.MouseStateProp;
        }
    }
}
