using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;
using System.Threading;
namespace Snakegame
{
    internal class Snake
    {
        public List<Point> body;
        public int direction;
        public int circleSize = 10;
        // snakeLength = number of small circles in snake
        public Snake(int snakeLength)
        {
            body = new List<Point>();
            direction = 1; // Start moving Right
            int startX = 200;
            int startY = 200;
            for (int i = 0; i < snakeLength; i++)
            {
                body.Add(new Point(startX - i * circleSize, startY));
            }

        }
        public void Grow(int amount)
        {
            for (int i = 0; i < amount; i++)
            {
                body.Add(body[body.Count - 1]);
            }
        }
        public void Move()
        {
            Point head = body[0];
            Point newHead = head;
            if (direction == 0) //Up
            {
                newHead.Y = newHead.Y - circleSize;
            }
            else if (direction == 1) //Right
            {
                newHead.X = newHead.X + circleSize;
            }
            else if (direction == 2) //Down
            {
                newHead.Y = newHead.Y + circleSize;
            }
            else if (direction == 3) //left
            {
                newHead.X = newHead.X - circleSize;
            }
            //Add new head.
            body.Insert(0, newHead);
            //Remove last circle.
            body.RemoveAt(body.Count - 1);
        }
        public void ChangeDirection(int newDirection)
        {
            if (direction == 0 && newDirection == 2)
            {
                return;
            }
            {
                if (direction == 1 && newDirection == 3)
                {
                    return;
                }
                if (direction == 3 && newDirection == 1)
                {
                    return;
                }
                direction = newDirection;

            }

        }
    }

}