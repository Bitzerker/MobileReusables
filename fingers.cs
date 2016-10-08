using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using UnityEngine;

namespace RoidRunner
{
    class fingers : MonoBehaviour

    {
        public LayerMask touchMask;
        private Vector2 tapLoc;
        private Vector2 newLoc;
        private Player player;
        private GameManager gMan;
        private UpgradeShop upShop;


        public enum charState { BLOCK, CHARGE, JUMP, SLIDE };
        public charState state;

        void Update()
        {
            foreach (Touch t in Input.touches)
            {
                Ray ray = Camera.main.ScreenPointToRay(t.position);
                RaycastHit hit;

                if(Physics.Raycast(ray, out hit, touchMask))
                {

                    GameObject touched = hit.transform.gameObject;

                    if(t.phase == TouchPhase.Began)
                    {
                        Vector2 tapLoc = t.position;
                        float counter = Time.time;
                    }
                    else if(t.phase == TouchPhase.Moved)
                    {
                        Vector2 newLoc = t.position;
                        Vector2 mathVec = new Vector2(newLoc.x - tapLoc.x, newLoc.y - tapLoc.y);
                        if(mathVec.y > 0) //UP > DOWN
                        {
                            if (mathVec.y > mathVec.x) //if UP is greater than ACROSS
                            {
                                state = charState.JUMP;
                            }
                            else if (mathVec.x > mathVec.y)//HOR > VER
                            {
                                if (mathVec.x > 0) //FORWARD > BACK
                                {
                                    state = charState.CHARGE;
                                }
                                else state = charState.BLOCK; //BACK > FORWARD

                            } ; 
                        }else if(mathVec.y <= 0) //DOWN > UP, gunnabe a NEG number.
                        {
                            if (mathVec.x > mathVec.y && mathVec.x != 0) //HOR > DOWN, HOR = positive.
                            {
                                state = charState.CHARGE;
                            }
                            else if (mathVec.x < 0 && mathVec.x > mathVec.y)//HOR > VER
                            {
                                state = charState.SLIDE;
                            }
                            else state = charState.BLOCK; //BACK > FORWARD

                            };
                        }
                        else if(t.phase == TouchPhase.Ended)
                        {
                            if (touched.tag == "Enemy" || touched.tag == "destructable")
                            {
                                player.slash_or_throw(touched);
                            }
                            else if (touched.tag == "UI")
                            {
                                if (touched.name == "PauseButton")
                                {
                                  gMan.doPause();
                                }
                                else if (touched.name == "MenuButton")
                                {
                                    gMan.promptMenu();
                                }
                            }
                            else if (touched.tag == "Shop")
                            {

                                if (touched.name == "UpgradeMelee")
                                {
                                    upShop.doMelee();
                                }
                                else if (touched.name == "UpgradeRanged")
                                {
                                    upShop.doRanged();
                                 }
                                else if (touched.name == "UpgradeDefence")
                                {
                                    upShop.doDefence();
                                }
                                else if (touched.name == "UpgradeHealth")
                                {
                                    player.currScore -= (100 - player.currHealth);
                                    player.currHealth += (100 - player.currHealth);
                                }

                            }
                        }
                    
                }
            }
        }
    }
}
