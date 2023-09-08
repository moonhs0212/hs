using EL_DC_Charger.common.item;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace EL_DC_Charger.EL_DC_Charger.Custom_UserControl.manager
{
    public class Manager_AdminEnter
    {
        public interface OnChange_AdminEnter
        {
            void onChange(List<Integer> touchList);
        }

        protected OnChange_AdminEnter mListener_OnChange = null;
        EL_Time mTime_LastTouch = new EL_Time();
        List<Integer> mList_Area = new List<Integer>();

        public void setOnChange_AdminEnter(OnChange_AdminEnter listener)
        {
            mListener_OnChange = listener;
        }

        int mMaxLength = 0;
        public Manager_AdminEnter(UserControl view, int maxLength)
        {
            mMaxLength = maxLength;

            if(view != null)
            {
                view.Click += new System.EventHandler(onTouch);
                view.DoubleClick += new System.EventHandler(onTouch);
            }

            width = 100;
            height = 100;
            left_startx = 0;
            left_stopx = left_startx + width;
            right_startx = view.Width - width;
            right_stopx = right_startx + width;
            top_starty = 0;
            top_stopy = 0 + height;
            bottom_starty = view.Height - height;
            bottom_stopy = bottom_starty + height;

        }

        public Manager_AdminEnter(PictureBox view, int maxLength)
        {
            mMaxLength = maxLength;

            if (view != null)
            {
                view.Click += new System.EventHandler(onTouch);
                view.DoubleClick += new System.EventHandler(onTouch);
            }

            width = 100;
            height = 100;
            left_startx = 0;
            left_stopx = left_startx + width;
            right_startx = view.Width - width;
            right_stopx = right_startx + width;
            top_starty = 0;
            top_stopy = 0 + height;
            bottom_starty = view.Height - height;
            bottom_stopy = bottom_starty + height;

        }

        private void onTouch(object sender, EventArgs e)
        {
            Control pic = (Control)sender;

            int x = Control.MousePosition.X;
            int y = Control.MousePosition.Y;

            Point mousePos = new Point(x, y); //프로그램 내 좌표
            Point mousePosPtoClient = pic.PointToClient(mousePos);  //picbox 내 좌표

            

            int area = getPositionArea(mousePosPtoClient.X, mousePosPtoClient.Y);

            Console.WriteLine(
                "area = " + area
                + ", ClickEvent mousePosPtoClient.x = " + mousePosPtoClient.X
                + ", mousePosPtoClient.y = " + mousePosPtoClient.Y
            );

            if (mList_Area.Count >= mMaxLength)
                mList_Area.RemoveAt(0);
            EL_Time time = new EL_Time();
            
            if(area< 1)
                mList_Area.Clear();
            else
            {
                double wastedSecond = mTime_LastTouch.getSecond_WastedTime(time);
                if(wastedSecond > 2)
                {
                    mList_Area.Clear();
                    mList_Area.Add(area);
                }else
                {
                    mList_Area.Add(area);
                    if(mList_Area.Count >= mMaxLength)
                    {
                        if(mListener_OnChange != null)
                            mListener_OnChange.onChange(mList_Area);

                    }
                }
            }

            mTime_LastTouch.setTime();



            List<string> strings = mList_Area.Select(i => i.ToString()).ToList();


            Console.WriteLine("mList_Area = " + String.Join(", ", strings));
        }

        public int getPositionArea(float x, float y)
        {
            int area = 0;

            if (x >= left_startx && x <= left_stopx)
            {
                if (y >= top_starty && y <= top_stopy)
                {
                    area = 1;
                }
                else if (y >= bottom_starty && y <= bottom_stopy)
                {
                    area = 3;
                }
            }
            else if (x >= right_startx && x <= right_stopx)
            {
                if (y >= top_starty && y <= top_stopy)
                {
                    area = 2;
                }
                else if (y >= bottom_starty && y <= bottom_stopy)
                {
                    area = 4;
                }
            }

            return area;
        }

        public void clearEvent()
        {
            mList_Area.Clear();
        }

        





        int width = 0;
        int height = 0;
        int left_startx = 0;
        int left_stopx = 0;
        int right_startx = 0;
        int right_stopx = 0;
        int top_starty = 0;
        int top_stopy = 0;
        int bottom_starty = 0;
        int bottom_stopy = 0;
    }

    
}
