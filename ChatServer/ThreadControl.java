import java.io.BufferedReader;
import java.io.InputStreamReader;
import java.io.PrintWriter;
import java.net.InetSocketAddress;
import java.net.ServerSocket;
import java.net.Socket;
class ThreadControl{
  public void Add_Player(Socket socket,int accessnumber){
    synchronized(this){
      if(accessnumber==0){
        System.out.println("プレイヤーがチャットに参加しました");
        threads[accessnumber]=new ServerThread(accessnumber,socket,this);
        threads[accessnumber].start();
        System.out.println("相手を探しています");
      }else{
        System.out.println("相手が見つかりました");
        threads[accessnumber]=new ServerThread(accessnumber,socket,this);
        threads[accessnumber].start();
      }
    }
  }
  public void Send_Mes(String mes){
    synchronized(this){
      for(int i=0;i<2;i++){
        threads[i].Return_Mes(mes);
      }
    }
  }
  ServerThread threads[]=new ServerThread[2];
}
