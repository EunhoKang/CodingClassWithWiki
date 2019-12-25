using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class IngameUI : MonoBehaviour
{
    public List<Image> imageFrame;
    public List<Sprite> commandImages;
    List<CommandNode> Queue=new List<CommandNode>();
    int index=0;

    void ImagePush(CommandNode node){
        if(index<imageFrame.Count){
            Sprite temp=null;
            if(node.GetName()=="MoveUp"){
                temp=commandImages[0];
            }
            else if(node.GetName()=="MoveDown"){
                temp=commandImages[1];
            }
            else if(node.GetName()=="MoveLeft"){
                temp=commandImages[2];
            }
            else if(node.GetName()=="MoveRight"){
                temp=commandImages[3];
            }
            imageFrame[index].sprite=temp;
            index++;
        }
    }
    void ImagePop(){
        if(index>0){
            imageFrame[index-1].sprite=null;
            index--;
        }
    }
    void ImageAllPop(){
        for(int i=0;i<imageFrame.Count;i++){
            imageFrame[i].sprite=null;
        }
        index=0;
    }
    public void NodePushed(string command){ //Change this after change the "CommandNode" code
        if(CharacterManager.manager.isMoving){
            return;
        }
        CommandNode temp=new CommandNode(command);
        Queue.Add(temp);
        ImagePush(temp);
    }

    public void SendQueue(){
        if(CharacterManager.manager.isMoving){
            return;
        }
        CharacterManager.manager.PlayerMove(new List<CommandNode>(Queue));
        ImageAllPop();
        Queue.Clear();
    }

    public void ResetQueue(){
        if(!CharacterManager.manager.isMoving){
            Queue.Clear();
            ImageAllPop();
        }
        CharacterManager.manager.ResetPlace();
    }
    public void PopQueue(){
        if(CharacterManager.manager.isMoving || Queue.Count<=0){
            return;
        }
        if(!CharacterManager.manager.isMoving){
            Queue.RemoveAt(Queue.Count-1);
            ImagePop();
        }
    }

    public void GameEnd(){
        //
    }
}
