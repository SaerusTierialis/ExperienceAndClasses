﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Terraria;
using Terraria.UI;

namespace ExperienceAndClasses.UI {
    class UIOverlay : UIStateCombo {
        public static readonly UIOverlay Instance = new UIOverlay();

        /*~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~ Constants ~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~*/
        private const float LEFT_FROM_MAX = 280f;
        private const float TOP_FROM_MAX = 40f;
        private const float TEXT_SCALE = 1.6f;
        private const float TEXT_SCALE_HOVER = TEXT_SCALE + 0.2f;

        /*~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~ Varibles ~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~*/
        private TextButton button;

        /*~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~ Initialize ~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~*/
        protected override void InitializeState() {
            button = new TextButton("Classes", TEXT_SCALE, TEXT_SCALE_HOVER);
            button.OnClick += new UIElement.MouseEvent(Click);
            state.Append(button);
        }

        /*~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~ Public ~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~*/

        public void Update(bool inventory_state) {
            if (ExperienceAndClasses.LOCAL_MPLAYER.show_classes_button) {
                Visibility = inventory_state;
                if (Visibility) {
                    button.Left.Set(Main.screenWidth - LEFT_FROM_MAX, 0f);
                    button.Top.Set(Main.screenHeight - TOP_FROM_MAX, 0f);
                    button.Recalculate();
                }
            }
            else {
                Visibility = false;
            }
        }

        /*~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~ Events ~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~*/

        private void Click(UIMouseEvent evt, UIElement listeningElement) {
            UIMain.Instance.Visibility = !UIMain.Instance.Visibility;
            UIHelpSettings.Instance.Visibility = false;
        }
    }
}
