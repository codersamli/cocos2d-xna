using cocos2d;

namespace tests
{
    public class MotionStreakTest : CCLayer
    {
        private static int sceneIdx = 0;
        private static int MAX_LAYER = 3;
        
        private string s_pPathB1 = "Images/b1";
        private string s_pPathB2 = "Images/b2";
        private string s_pPathF1 = "Images/f1";
        private string s_pPathF2 = "Images/f2";
        private string s_pPathR1 = "Images/r1";
        private string s_pPathR2 = "Images/r2";

        private const int kTagLabel = 2;

        protected CCMotionStreak streak;

        public static CCLayer createMotionLayer(int nIndex)
        {
            switch (nIndex)
            {
                case 0:
                    return new MotionStreakTest1();
                case 1:
                    return new MotionStreakTest2();
                case 2:
                    return new Issue1358();
            }

            return null;
        }


        public static CCLayer nextMotionAction()
        {
            sceneIdx++;
            sceneIdx = sceneIdx % MAX_LAYER;

            CCLayer pLayer = createMotionLayer(sceneIdx);
            return pLayer;
        }

        public static CCLayer backMotionAction()
        {
            sceneIdx--;
            int total = MAX_LAYER;
            if (sceneIdx < 0)
                sceneIdx += total;

            CCLayer pLayer = createMotionLayer(sceneIdx);
            return pLayer;
        }

        public static CCLayer restartMotionAction()
        {
            CCLayer pLayer = createMotionLayer(sceneIdx);
            return pLayer;
        }

        public virtual string title()
        {
            return "No title";
        }

        public virtual string subtitle()
        {
            return "";
        }

        public override void OnEnter()
        {
            base.OnEnter();

            CCSize s = CCDirector.SharedDirector.WinSize;

            var label = CCLabelTTF.Create(title(), "arial", 32);
            AddChild(label, 0, kTagLabel);
            label.Position = new CCPoint(s.width / 2, s.height - 50);

            string subTitle = this.subtitle();
            if (subTitle.Length > 0)
            {
                var l = CCLabelTTF.Create(subTitle, "arial", 16);
                AddChild(l, 1);
                l.Position = new CCPoint(s.width / 2, s.height - 80);
            }

            var item1 = CCMenuItemImage.Create(s_pPathB1, s_pPathB2, backCallback);
            var item2 = CCMenuItemImage.Create(s_pPathR1, s_pPathR2, restartCallback);
            var item3 = CCMenuItemImage.Create(s_pPathF1, s_pPathF2, nextCallback);

            var menu = CCMenu.Create(item1, item2, item3);

            menu.Position = CCPoint.Zero;
            item1.Position = new CCPoint(s.width / 2 - item2.ContentSize.width * 2, item2.ContentSize.height / 2);
            item2.Position = new CCPoint(s.width / 2, item2.ContentSize.height / 2);
            item3.Position = new CCPoint(s.width / 2 + item2.ContentSize.width * 2, item2.ContentSize.height / 2);

            AddChild(menu, 1);

            var itemMode = CCMenuItemToggle.Create(modeCallback,
                                                   CCMenuItemFont.Create("Use High Quality Mode"),
                                                   CCMenuItemFont.Create("Use Fast Mode")
                );

            var menuMode = CCMenu.Create(itemMode);
            AddChild(menuMode);

            menuMode.Position = new CCPoint(s.width / 2, s.height / 4);
        }

        private void modeCallback(CCObject pSender)
        {
            bool fastMode = streak.FastMode;
            streak.FastMode = !fastMode;
        }

        public void restartCallback(CCObject pSender)
        {
            CCScene s = new MotionStreakTestScene(); 
            s.AddChild(restartMotionAction());
            CCDirector.SharedDirector.ReplaceScene(s);
        }

        public void nextCallback(CCObject pSender)
        {
            CCScene s = new MotionStreakTestScene(); 
            s.AddChild(nextMotionAction());
            CCDirector.SharedDirector.ReplaceScene(s);
        }

        public void backCallback(CCObject pSender)
        {
            CCScene s = new MotionStreakTestScene(); 
            s.AddChild(backMotionAction());
            CCDirector.SharedDirector.ReplaceScene(s);
        }
    }
}