﻿using System;
using static UnityEngine.Rendering.DebugUI;

public class Game
{
	readonly int numberPoolSize = 4;
	readonly int maxNumber = 12;
	// Variables (Game Board, Players)
	protected internal Player p1;
	protected internal Player p2;
    protected internal Player curMove;
    protected internal (int,int)[,] board;
	

    public Game(int dim)
	{
        p1 = new Player(1,this);
		p2 = new Player(2,this);
		curMove = p1;
		board = new (int,int)[dim,dim];
		for (int i = 0; i < dim; i++) {
			for (int j = 0; j < dim; j++) {
				board[i, j] = (-1,0);
			}
		}
        Console.WriteLine("Game was created!");
	}
	public bool MakeMove(Player p, (int,int) position, int number) {
		if (!IsValid(p,position, number))
		{
			Console.WriteLine("This move is not valid!");
			return false;
		}
		board[position.Item1, position.Item2] = (number, p.getId());
		p.replaceNum(number);
		p1.setScore(ComputeScore(p1));
		p2.setScore(ComputeScore(p2));
		if (p.Equals(p1))
		{
			curMove = p2;
		}
		else {
			curMove = p1;
		}
		//Call Functions in UI (add number, change scores, change turn)

		return true;
	}
	public Player TerminateGame() {
		if (p1.getScore() > p2.getScore())
		{
			return p1;
		}
		else if (p2.getScore() > p1.getScore())
		{
			return p2;
		}
		else {
			return null;
		}
	}
	protected internal bool IsValid(Player p, (int, int) position, int number) {
        if (p.Equals(curMove) && p.hasNum(number))
		{
			return true;
		}
		else {
			return false;
		}
		
	}
    protected internal int ComputeScore(Player p) {
		return 0;
	}

	public int getNumberPoolSize() {
		return numberPoolSize;
	}
	public int getMaxNumber() {
		return maxNumber;
	}
}
