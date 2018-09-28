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
      int player_sum=0;
      while(player_sum<8){
        try  {
          socket = server.accept();
          player_sum++;
          controller.Add_Player(socket,player_sum);
        }catch(Exception e){
          System.out.println("接続に失敗しました");
          System.exit(0);
        }
      }
      System.out.println("人数が上限に達しました");
    } catch (Exception e) {
      System.out.println("接続が切れました");
      System.exit(0);
    }
  }
}
