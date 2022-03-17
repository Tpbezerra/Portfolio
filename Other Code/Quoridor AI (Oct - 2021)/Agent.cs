using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;

class Agent : BaseAgent
{
    [STAThread]
    static void Main()
    {
        Program.Start(new Agent());
    }

    public Agent() { }

    public override Drag SökNästaDrag(SpelBräde bräde)
    {
        Graph boardGraph = CreateGraph(bräde);

        //Så ni kan kolla om systemet fungerar!
        Spelare jag = bräde.spelare[0];
        Point playerPos = jag.position;
        Spelare opponent = bräde.spelare[1];
        Point opponentPos = opponent.position;
        
        Drag drag = new Drag();

        Stack<int> myPath = FindShortestPath(boardGraph, playerPos, 8);
        Stack<int> opponentPath = FindShortestPath(boardGraph, opponentPos, 0);

        if (opponentPath.Count < myPath.Count && jag.antalVäggar > 0)
        {
            // Opponent is closer, consider placing wall.
            drag = PlaceWall(bräde, boardGraph, opponentPath, playerPos, opponentPos, opponentPath.Count / (float)myPath.Count);
            if (drag.point != new Point(-1, -1))
                return drag;
        }

        myPath.Pop();
        int targetVertex = myPath.Pop();

        Point targetPos = new Point();
        targetPos.Y = (int)Math.Floor(targetVertex / 9f);
        targetPos.X = targetVertex - (targetPos.Y * 9);

        drag.typ = Typ.Flytta;
        drag.point = targetPos;

        return drag;
    }

    Stack<int> FindShortestPath(Graph boardGraph, Point fromPos, int toY)
    {
        int shortestPath = int.MaxValue;
        int currentPath;

        Stack<int> minPath;
        Stack<int> toReturn = null;

        boardGraph.Bfp(fromPos.Y * 9 + fromPos.X);

        for (int x = 0; x < 9; x++)
        {
            minPath = boardGraph.PathTo(toY * 9 + x);
            if (minPath == null)
                continue;

            currentPath = minPath.Count;

            if (currentPath < shortestPath)
            {
                toReturn = minPath;
                shortestPath = currentPath;
            }
        }

        return toReturn;
    }

    Drag PlaceWall(SpelBräde bräde, Graph boardGraph, Stack<int> opponentPath, Point myPos, Point opponentPos, float originalPathValue)
    {
        Drag drag = new Drag();

        int fromVertex = opponentPath.Pop();
        int toVertex;
        int difference;
        
        float highestWallValue = -1;

        Point fromPosition;
        Point dragPosition = new Point(-1, -1);

        Typ dragTyp = Typ.Horisontell;

        bool negative;
        bool positive;

        while (opponentPath.Count > 0)
        {
            toVertex = opponentPath.Pop();

            fromPosition.Y = (int)Math.Floor(fromVertex / 9f);
            fromPosition.X = fromVertex - (fromPosition.Y * 9);

            difference = toVertex - fromVertex;

            positive = false;
            negative = false;

            if (Math.Abs(difference) > 1)
            {
                // Leta efter horisontella väggar.
                if (difference > 0) // Upp.
                {
                    if (fromPosition.X < 8 && !(bräde.vertikalaVäggar[fromPosition.X, fromPosition.Y] && bräde.vertikalaVäggar[fromPosition.X, fromPosition.Y + 1]) && 
                        !bräde.horisontellaVäggar[fromPosition.X + 1, fromPosition.Y])
                    {
                        drag.typ = Typ.Horisontell;
                        drag.point = fromPosition;

                        positive = true;
                    }
                    if (fromPosition.X > 0 && !(bräde.vertikalaVäggar[fromPosition.X - 1, fromPosition.Y] && bräde.vertikalaVäggar[fromPosition.X - 1, fromPosition.Y + 1]) && 
                        !bräde.horisontellaVäggar[fromPosition.X - 1, fromPosition.Y])
                    {
                        drag.typ = Typ.Horisontell;
                        drag.point = fromPosition;

                        negative = true;
                    }
                }
                else // Ner.
                {
                    if (fromPosition.X < 8 && !(bräde.vertikalaVäggar[fromPosition.X, fromPosition.Y] && bräde.vertikalaVäggar[fromPosition.X, fromPosition.Y - 1]) && 
                        !bräde.horisontellaVäggar[fromPosition.X + 1, fromPosition.Y - 1])
                    {
                        drag.typ = Typ.Horisontell;
                        drag.point = new Point(fromPosition.X, fromPosition.Y - 1);

                        positive = true;
                    }
                    if (fromPosition.X > 0 && !(bräde.vertikalaVäggar[fromPosition.X - 1, fromPosition.Y] && bräde.vertikalaVäggar[fromPosition.X - 1, fromPosition.Y - 1]) && 
                        !bräde.horisontellaVäggar[fromPosition.X - 1, fromPosition.Y - 1])
                    {
                        drag.typ = Typ.Horisontell;
                        drag.point = new Point(fromPosition.X, fromPosition.Y - 1);

                        negative = true;
                    }
                }
            }
            else
            {
                // Leta efter vertikala väggar.
                if (difference > 0) // Höger.
                {
                    if (fromPosition.Y < 8 && !(bräde.horisontellaVäggar[fromPosition.X, fromPosition.Y] && bräde.horisontellaVäggar[fromPosition.X + 1, fromPosition.Y]) && 
                        !bräde.vertikalaVäggar[fromPosition.X, fromPosition.Y + 1])
                    {
                        drag.typ = Typ.Vertikal;
                        drag.point = fromPosition;

                        positive = true;
                    }
                    if (fromPosition.Y > 0 && !(bräde.horisontellaVäggar[fromPosition.X, fromPosition.Y - 1] && bräde.horisontellaVäggar[fromPosition.X + 1, fromPosition.Y - 1]) && 
                        !bräde.vertikalaVäggar[fromPosition.X, fromPosition.Y - 1])
                    {
                        drag.typ = Typ.Vertikal;
                        drag.point = fromPosition;

                        negative = true;
                    }
                }
                else // Vänster.
                {
                    if (fromPosition.Y < 8 && !(bräde.horisontellaVäggar[fromPosition.X, fromPosition.Y] && bräde.horisontellaVäggar[fromPosition.X - 1, fromPosition.Y]) && 
                        !bräde.vertikalaVäggar[fromPosition.X - 1, fromPosition.Y + 1])
                    {
                        drag.typ = Typ.Vertikal;
                        drag.point = new Point(fromPosition.X - 1, fromPosition.Y);

                        positive = true;
                    }
                    if (fromPosition.Y > 0 && !(bräde.horisontellaVäggar[fromPosition.X, fromPosition.Y - 1] && bräde.horisontellaVäggar[fromPosition.X - 1, fromPosition.Y - 1]) && 
                        !bräde.vertikalaVäggar[fromPosition.X - 1, fromPosition.Y - 1])
                    {
                        drag.typ = Typ.Vertikal;
                        drag.point = new Point(fromPosition.X - 1, fromPosition.Y);

                        negative = true;
                    }
                }
            }

            if (positive || negative)
            {
                float wallValue = ConfirmWallValue(boardGraph, fromVertex, toVertex, ref positive, ref negative, myPos, opponentPos, drag);

                if (wallValue > highestWallValue && wallValue > originalPathValue)
                {
                    dragPosition = drag.point;
                    if (drag.typ == Typ.Horisontell && negative)
                        dragPosition.X--;
                    else if (drag.typ == Typ.Vertikal && negative)
                        dragPosition.Y--;

                    dragTyp = drag.typ;

                    highestWallValue = wallValue;
                }
            }

            fromVertex = toVertex;
        }

        drag.point = dragPosition;
        drag.typ = dragTyp;

        return drag;
    }

    float ConfirmWallValue(Graph boardGraph, int fromVertex, int toVertex, ref bool positive, ref bool negative, Point myPos, Point opponentPos, Drag drag)
    {
        float toReturn = -1;

        boardGraph.RemoveEdge(fromVertex, toVertex);

        Stack<int> tempPlayerPath = FindShortestPath(boardGraph, myPos, 8);
        Stack<int> tempOppPath = FindShortestPath(boardGraph, opponentPos, 0);
        if (tempPlayerPath != null && tempOppPath != null)
        {
            Stack<int> positiveOppPath = null;
            Stack<int> negativeOppPath = null;
            Stack<int> positivePlayerPath = null;
            Stack<int> negativePlayerPath = null;
            if (drag.typ == Typ.Horisontell)
            {
                if (positive)
                {
                    boardGraph.RemoveEdge(fromVertex + 1, toVertex + 1);
                    positiveOppPath = FindShortestPath(boardGraph, opponentPos, 0);
                    positivePlayerPath = FindShortestPath(boardGraph, myPos, 8);
                    boardGraph.AddEdge(fromVertex + 1, toVertex + 1);
                }
                if (negative)
                {
                    boardGraph.RemoveEdge(fromVertex - 1, toVertex - 1);
                    negativeOppPath = FindShortestPath(boardGraph, opponentPos, 0);
                    negativePlayerPath = FindShortestPath(boardGraph, myPos, 8);
                    boardGraph.AddEdge(fromVertex - 1, toVertex - 1);
                }
            }
            else
            {
                if (positive)
                {
                    boardGraph.RemoveEdge(fromVertex + 9, toVertex + 9);
                    positiveOppPath = FindShortestPath(boardGraph, opponentPos, 0);
                    positivePlayerPath = FindShortestPath(boardGraph, myPos, 8);
                    boardGraph.AddEdge(fromVertex + 9, toVertex + 9);
                }
                if (negative)
                {
                    boardGraph.RemoveEdge(fromVertex - 9, toVertex - 9);
                    negativeOppPath = FindShortestPath(boardGraph, opponentPos, 0);
                    negativePlayerPath = FindShortestPath(boardGraph, myPos, 8);
                    boardGraph.AddEdge(fromVertex - 9, toVertex - 9);
                }
            }

            if (positiveOppPath != null && positivePlayerPath != null && negativeOppPath != null && negativePlayerPath != null)
            {
                if (positiveOppPath.Count / (float)positivePlayerPath.Count > negativeOppPath.Count / (float)negativePlayerPath.Count)
                {
                    tempOppPath = positiveOppPath;
                    tempPlayerPath = positivePlayerPath;
                    negative = false;
                }
                else
                {
                    tempOppPath = negativeOppPath;
                    tempPlayerPath = negativePlayerPath;
                    positive = false;
                }
            }
            else if ((positiveOppPath != null && positivePlayerPath != null) || (negativeOppPath != null && negativePlayerPath != null))
            {
                if (positiveOppPath != null && positivePlayerPath != null)
                {
                    tempOppPath = positiveOppPath;
                    tempPlayerPath = positivePlayerPath;
                    negative = false;
                }
                else
                {
                    tempOppPath = negativeOppPath;
                    tempPlayerPath = negativePlayerPath;
                    positive = false;
                }
            }

            if ((positiveOppPath != null && positivePlayerPath != null) || (negativeOppPath != null && negativePlayerPath != null))
                toReturn = tempOppPath.Count / (float)tempPlayerPath.Count;
        }

        boardGraph.AddEdge(fromVertex, toVertex);

        return toReturn;
    }

    Graph CreateGraph(SpelBräde bräde)
    {
        Graph toReturn = new Graph(9 * 9);

        int currentVertex;
        for (int x = 0; x < 9; x++)
        {
            for (int y = 0; y < 9; y++)
            {
                currentVertex = y * 9 + x;
                if (y < 8 && !bräde.horisontellaVäggar[x, y])
                    toReturn.AddEdge(currentVertex, currentVertex + 9);
                if (y > 0 && !bräde.horisontellaVäggar[x, y - 1])
                    toReturn.AddEdge(currentVertex, currentVertex - 9);

                if (x < 8 && !bräde.vertikalaVäggar[x, y])
                    toReturn.AddEdge(currentVertex, currentVertex + 1);
                if (x > 0 && !bräde.vertikalaVäggar[x - 1, y])
                    toReturn.AddEdge(currentVertex, currentVertex - 1);
            }
        }

        return toReturn;
    }

    public override Drag GörOmDrag(SpelBräde bräde, Drag drag)
    {
        //Om draget ni försökte göra var felaktigt så kommer ni hit
        System.Diagnostics.Debugger.Break();    //Brytpunkt
        return SökNästaDrag(bräde);
    }
}