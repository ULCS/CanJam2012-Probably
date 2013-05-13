/*          Collision Library
 * 
 * Author: David Whitehead
 * Date: 20/02/2011
 * */


using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
//This is my own personal Collision library that i have written and extracted into the game.
namespace Collision
{
    public class objectCollision
    {
        //Here is where collision for objects will be done for the library.
        public static Boolean collision(double ob1Left, double ob1Top, double ob1Right, double ob1Bottom, double ob2Left, double ob2Top, double ob2Right, double ob2Bottom, double movementX, double movementY)
        {
            if (ob1Left + movementX >= ob2Left && ob1Left + movementX <= ob2Right || ob1Right + movementX >= ob2Left && ob1Right + movementX <= ob2Right) //Left and Right check
                if (ob1Top + movementY >= ob2Top && ob1Top + movementY <= ob2Bottom || ob1Bottom + movementY >= ob2Top && ob1Bottom + movementY <= ob2Bottom)//Top and Bottom Check
                    return true;
                else
                    return false;
            else
                return false;
        }

        public static Boolean fullSearchCollision(double ob1Left, double ob1Top, double ob1Right, double ob1Bottom, double ob2Left, double ob2Top, double ob2Right, double ob2Bottom, double movementX, double movementY)
        {
            for (double k = ob1Left; k <= ob1Right; k++)
            {
                if (k >= ob2Left && k <= ob2Right)
                {
                    for (double j = ob1Top; j < ob1Bottom; j++)
                    {
                        if (j >= ob2Top && j <= ob2Bottom)
                        {
                            return true;
                        }
                    }
                }
            }
            return false;
        }


        public static Boolean partialCollisionLeft(double ob1Right, double ob2Left, double movement)
        {
            if (ob1Right + movement >= ob2Left)
                return true;
            else
                return false;
        }

        public static Boolean partialCollisionRight(double ob1Left, double ob2Right, double movement)
        {
            if (ob1Left + movement <= ob2Right)
                return true;
            else
                return false;
        }

        public static Boolean partialCollisionTop(double ob1Bottom, double ob2Top, double movement)
        {
            if (ob1Bottom + movement <= ob2Top)
                return true;
            else
                return false;
        }

        public static Boolean partialCollisionBottom(double ob1Top, double ob2Bottom, double movement)
        {
            if (ob1Top + movement >= ob2Bottom)
                return true;
            else
                return false;
        }
    }

    public class borderDetection
    {

        public static Boolean mainBorder(double leftSide, double rightSide, double bottomSide, double topSide, double maxX, double maxY, double movement)
        {

            if (rightSide + movement > maxX)
                return true;
            else if (bottomSide + movement > maxY)
                return true;
            else if (leftSide + movement < 0)
                return true;
            else if (topSide + movement < 0)
                return true;
            else
                return false;
        }

        public static Boolean partialBorderLeft(double leftSide, double movement)
        {
            if (leftSide + movement < 0)
                return true;
            else
                return false;
        }

        public static Boolean partialBorderRight(double rightSide, double maxX, double movement)
        {
            if (rightSide + movement > maxX)
                return true;
            else
                return false;
        }

        public static Boolean partialBorderBottom(double bottomSide, double maxY, double movement)
        {
            if (bottomSide + movement > maxY)
                return true;
            else
                return false;
        }

    }

    public class mouseCollision
    {

        public static Boolean Collision(double mouseX, double mouseY, double objectX, double objectY, double objectRight, double objectBotom)
        {
            if (mouseX >= objectX && mouseX <= objectRight)
            {
                if (mouseY >= objectY && mouseY <= objectBotom)
                {
                    return true;
                }
                else
                    return false;
            }
            else
                return false;
        }


    }
}
