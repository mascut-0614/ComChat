import java.io.InputStreamReader;
import java.net.InetSocketAddress;
import java.net.InetAddress;
import java.net.ServerSocket;
import java.net.Socket;
public class Test{
  public static void main(String[] args){
    ThreadControl controller=new ThreadControl();
    try (ServerSocket server = new ServerSocket()) {
      InetAddress addr=InetAddress.getLocalHost();
      System.out.println(addr.getHostAddress());
      server.bind(new InetSocketAddress(addr.getHostAddress(), 8080));
      Socket socket;
      System.out.println("接続要求を待ちます。");
      for(int i=0;i<=1;i++){
        try  {
          socket = server.accept();
          controller.Add_Player(socket,i);
        }catch(Exception e){
          System.out.println("プレイヤーが揃いませんでした");
          System.exit(0);
        }
      }
    } catch (Exception e) {
      System.out.println("接続が切れました");
      System.exit(0);
    }
  }
}
