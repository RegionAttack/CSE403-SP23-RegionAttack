using System;
using System.Collections.Generic;

public class GameOption
{
    String mode;
    int dim;
    bool startingPlayer;
    bool onlineMode;
    bool computerOpponent;
    int cp = 0;
    bool localGame;
    public GameOption(int dim, bool startingPlayer, String mode)
	{
        this.mode = mode;
        this.dim = dim;
        this.startingPlayer = startingPlayer;
		if(mode == "local"){
            onlineMode = false;
            computerOpponent = false;
            cp = 0;
            localGame = true;
        }else if(mode == "online"){
            onelineMode = true;
            computerOpponent = false;
            cp = 0;
            localGame = false;
        }else if(mode == "computer1") {
            onelineMode = false;
            computerOpponent = true;
            cp = 1;
            localGame = false;
        }else if(mode == "computer2"){
            onelineMode = false;
            computerOpponent = true;
            cp = 2;
            localGame = false;
        }
	}
    public Game initGame(){
        Game game = new Game(dim, isComputerGame);
        game.isP1Turn = startingPlayer;
        return game;
    }
    public bool isOnlineGame(){
        return onlineMode;
    }
    public bool isLocalGame(){
        return localGame;
    }
    public int isComputerGame(){
        return cp;
    }
	
}
