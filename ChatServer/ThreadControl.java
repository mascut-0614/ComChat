import java.io.BufferedReader;
import java.io.InputStreamReader;
import java.io.PrintWriter;
import java.net.InetSocketAddress;
import java.net.ServerSocket;
import java.net.Socket;
class ThreadControl{
  int player_sum=0;
  public void Add_Player(Socket socket,int accessnumber){
    synchronized(this){
      System.out.println("プレイヤーがチャットに参加しました");
      player_sum++;
      threads[accessnumber]=new ServerThread(accessnumber,socket,this);
      threads[accessnumber].start();
    }
  }
  public void Send_Mes(String mes){
    synchronized(this){
      for(int i=1;i<=player_sum;i++){
        threads[i].Return_Mes(mes);
      }
    }
  }
  ServerThread threads[]=new ServerThread[9];
}
