import java.io.BufferedReader;
import java.io.InputStreamReader;
import java.io.PrintWriter;
import java.io.OutputStream;
import java.net.InetSocketAddress;
import java.net.ServerSocket;
import java.net.Socket;
import java.lang.String;
class ServerThread extends Thread{
  private Socket socket=null;
  int accessnumber;
  ThreadControl controller;
  //PrintWriter writer = new PrintWriter(socket.getOutputStream(),true);
  public ServerThread(int accessnumber,Socket socket,ThreadControl controller){
    this.accessnumber=accessnumber;
    this.socket=socket;
    this.controller=controller;
  }
  public void run(){
    if(socket==null){
      return;
    }else{
      System.out.printf("%d人目の参加者です。\n",accessnumber);
    }
    Return_Mes("Start,"+Integer.toString(accessnumber)+"|");
    try(BufferedReader reader = new BufferedReader(
        new InputStreamReader(socket.getInputStream()))){
          while(true){
              String line=reader.readLine();
              if(line!=null){
                this.controller.Send_Mes(line);
              }else {
                break;
              }
          }
        }catch(Exception e){
          e.printStackTrace();
        }
  }
  public void Return_Mes(String mes){
    try{
      //writer.printf("%s\n",mes);
      OutputStream OS=socket.getOutputStream();
      OS.write(mes.getBytes());
    }catch(Exception e){
      System.exit(0);
    }
  }
}
