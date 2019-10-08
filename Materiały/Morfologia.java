package javafxapplication1;

import java.awt.Color;
import java.awt.image.BufferedImage;
import java.io.File;
import java.io.IOException;
import javafx.embed.swing.SwingFXUtils;
import javafx.event.ActionEvent;
import javafx.scene.Scene;
import javafx.scene.control.Button;
import javafx.scene.image.Image;
import javafx.scene.image.ImageView;
import javafx.scene.layout.HBox;
import javafx.scene.layout.VBox;
import javax.imageio.ImageIO;
import javax.swing.JFileChooser;

public class Morfologia extends Scene {
    VBox vBox;
    BufferedImage img = null;
    int myThreshold = 200;
    int []r = new int[256];
    int []g = new int[256];
    int []b = new int[256];
    ImageView iV = new ImageView();
    HBox hBox = new HBox();
    Button openButton = new Button("open image");
    Button backToMain = new Button("back to main window");
    Button dylatacja = new Button("dylatacja");
    Button erozja = new Button("erozja");
    Button otwarcie = new Button("otwarcie");
    Button zamkniecie = new Button("zamkniecie");
    Button pocienianie = new Button("pocienianie");
    Button pogrubianie = new Button("pogrubianie");

    public Morfologia(VBox vBox, int w, int h){
        super(vBox, w, h);
        this.vBox = vBox;
        hBox = new HBox();
        MainWindow.mainStage.setTitle("Zadanie 1");
        hBox.getChildren().addAll(openButton,backToMain,dylatacja,erozja,otwarcie,zamkniecie,pocienianie,pogrubianie);
        vBox.getChildren().addAll(hBox,iV);            
             
        setListeners();
    }
    
    private void dylatacja() {
        boolean [][]temp = new boolean[img.getWidth()][img.getHeight()];
        for (int i = 1; i<img.getWidth()-1;++i) {
            for (int j = 1; j<img.getHeight()-1;++j) {
                Color c;
                
                if (new Color(img.getRGB(i-1, j-1)).equals(Color.BLACK)){
                    temp[i][j] = true;
                }
                if (new Color(img.getRGB(i-1, j)).equals(Color.BLACK)){
                    temp[i][j] = true;
                }
                if (new Color(img.getRGB(i, j-1)).equals(Color.BLACK)){
                    temp[i][j] = true;
                }
                if (new Color(img.getRGB(i+1, j+1)).equals(Color.BLACK)){
                    temp[i][j] = true;
                }
                if (new Color(img.getRGB(i+1, j)).equals(Color.BLACK)){
                    temp[i][j] = true;
                }
                if (new Color(img.getRGB(i, j+1)).equals(Color.BLACK)){
                    temp[i][j] = true;
                }
                if (new Color(img.getRGB(i+1, j-1)).equals(Color.BLACK)){
                    temp[i][j] = true;
                }
                if (new Color(img.getRGB(i-1, j+1)).equals(Color.BLACK)){
                    temp[i][j] = true;
                }
            }
        }
        for (int i = 0; i<img.getWidth();++i) {
            for (int j = 0; j<img.getHeight();++j) {
                if(temp[i][j] == true) {
                    img.setRGB(i, j, Color.BLACK.getRGB());
                }
            }
        }
        reloadImage();
    }
    private void otwarcie(){
        erozja();
        dylatacja();
    } 
    private void zamkniecie(){
        dylatacja();
        erozja();
    }
    private void pogrubianie(){
        boolean [][]temp = new boolean[img.getWidth()][img.getHeight()];
        for (int i = 1; i<img.getWidth()-1;++i) {
            for (int j = 1; j<img.getHeight()-1;++j) {
                Color []mask = new Color[9];
                mask[0]=Color.BLACK;
                mask[1]=Color.BLACK;
                mask[2]=Color.RED;
                mask[3]=Color.BLACK;
                mask[4]=Color.WHITE;
                mask[5]=Color.RED;
                mask[6]=Color.BLACK;
                mask[7]=Color.RED;
                mask[8]=Color.WHITE;
                if(!check(i,j,mask)){
//                    System.out.println(i);
//                    System.out.println(j);
                    temp[i][j]=true;
                }
            }
        }

        for (int i = 1; i<img.getWidth()-1;++i) {
            for (int j = 1; j<img.getHeight()-1;++j) {
                Color []mask = new Color[9];
                mask[0]=Color.RED;
                mask[1]=Color.RED;
                mask[2]=Color.WHITE;
                mask[3]=Color.BLACK;
                mask[4]=Color.WHITE;
                mask[5]=Color.RED;
                mask[6]=Color.BLACK;
                mask[7]=Color.BLACK;
                mask[8]=Color.BLACK;
                if(!check(i,j,mask)){
//                    System.out.println(i);
//                    System.out.println(j);
                    temp[i][j]=true;
                }
            }
        }

        for (int i = 1; i<img.getWidth()-1;++i) {
            for (int j = 1; j<img.getHeight()-1;++j) {
                Color []mask = new Color[9];
                mask[0]=Color.WHITE;
                mask[1]=Color.RED;
                mask[2]=Color.BLACK;
                mask[3]=Color.RED;
                mask[4]=Color.WHITE;
                mask[5]=Color.BLACK;
                mask[6]=Color.RED;
                mask[7]=Color.BLACK;
                mask[8]=Color.BLACK;
                if(!check(i,j,mask)){
//                    System.out.println(i);
//                    System.out.println(j);
                    temp[i][j]=true;
                }
            }
        }

        for (int i = 1; i<img.getWidth()-1;++i) {
            for (int j = 1; j<img.getHeight()-1;++j) {
                Color []mask = new Color[9];
                mask[0]=Color.BLACK;
                mask[1]=Color.BLACK;
                mask[2]=Color.BLACK;
                mask[3]=Color.RED;
                mask[4]=Color.WHITE;
                mask[5]=Color.BLACK;
                mask[6]=Color.WHITE;
                mask[7]=Color.RED;
                mask[8]=Color.RED;
                if(!check(i,j,mask)){
//                    System.out.println(i);
//                    System.out.println(j);
                    temp[i][j]=true;
                }
            }
        }
        ///////PIATA
        for (int i = 1; i<img.getWidth()-1;++i) {
            for (int j = 1; j<img.getHeight()-1;++j) {
                Color []mask = new Color[9];
                mask[0]=Color.RED;
                mask[1]=Color.BLACK;
                mask[2]=Color.BLACK;
                mask[3]=Color.RED;
                mask[4]=Color.WHITE;
                mask[5]=Color.BLACK;
                mask[6]=Color.WHITE;
                mask[7]=Color.RED;
                mask[8]=Color.BLACK;
                if(!check(i,j,mask)){
//                    System.out.println(i);
//                    System.out.println(j);
                    temp[i][j]=true;
                }
            }
        }

        for (int i = 1; i<img.getWidth()-1;++i) {
            for (int j = 1; j<img.getHeight()-1;++j) {
                Color []mask = new Color[9];
                mask[0]=Color.BLACK;
                mask[1]=Color.BLACK;
                mask[2]=Color.BLACK;
                mask[3]=Color.BLACK;
                mask[4]=Color.WHITE;
                mask[5]=Color.RED;
                mask[6]=Color.RED;
                mask[7]=Color.RED;
                mask[8]=Color.WHITE;
                if(!check(i,j,mask)){
//                    System.out.println(i);
//                    System.out.println(j);
                    temp[i][j]=true;
                }
            }
        }

        for (int i = 1; i<img.getWidth()-1;++i) {
            for (int j = 1; j<img.getHeight()-1;++j) {
                Color []mask = new Color[9];
                mask[0]=Color.BLACK;
                mask[1]=Color.RED;
                mask[2]=Color.WHITE;
                mask[3]=Color.BLACK;
                mask[4]=Color.WHITE;
                mask[5]=Color.RED;
                mask[6]=Color.BLACK;
                mask[7]=Color.BLACK;
                mask[8]=Color.RED;
                if(!check(i,j,mask)){
//                    System.out.println(i);
//                    System.out.println(j);
                    temp[i][j]=true;
                }
            }
        }
        for (int i = 1; i<img.getWidth()-1;++i) {
            for (int j = 1; j<img.getHeight()-1;++j) {
                Color []mask = new Color[9];
                mask[0]=Color.WHITE;
                mask[1]=Color.RED;
                mask[2]=Color.RED;
                mask[3]=Color.RED;
                mask[4]=Color.WHITE;
                mask[5]=Color.BLACK;
                mask[6]=Color.BLACK;
                mask[7]=Color.BLACK;
                mask[8]=Color.BLACK;
                if(!check(i,j,mask)){
//                    System.out.println(i);
//                    System.out.println(j);
                    temp[i][j]=true;
                }
            }
        }
        for (int i = 0; i<img.getWidth();++i) {
            for (int j = 0; j<img.getHeight();++j) {
                if(temp[i][j] == true) {
//                    System.out.println(i);
//                    System.out.println(j);
                    System.out.println(new Color(img.getRGB(i, j)).getRed());
                    img.setRGB(i, j, Color.BLACK.getRGB());
                    System.out.println(new Color(img.getRGB(i, j)).getRed());
                    System.out.println(new Color(img.getRGB(i, j)).getGreen());
                    System.out.println(new Color(img.getRGB(i, j)).getBlue());

                }
            }
        }
                
        reloadImage();
    }
    private void pocienianie(){
        boolean [][]temp = new boolean[img.getWidth()][img.getHeight()];
        Color []mask = new Color[9];
                mask[0]=Color.WHITE;
                mask[1]=Color.WHITE;
                mask[2]=Color.WHITE;
                mask[3]=Color.RED;
                mask[4]=Color.BLACK;
                mask[5]=Color.RED;
                mask[6]=Color.BLACK;
                mask[7]=Color.BLACK;
                mask[8]=Color.BLACK;
        for (int i = 1; i<img.getWidth()-1;++i) {
            for (int j = 1; j<img.getHeight()-1;++j) {
                if(!check(i,j,mask)){
                    temp[i][j]=true;
                }
            }
        }

        mask = new Color[9];
        mask[0]=Color.WHITE;
        mask[1]=Color.RED;
        mask[2]=Color.BLACK;
        mask[3]=Color.WHITE;
        mask[4]=Color.BLACK;
        mask[5]=Color.BLACK;
        mask[6]=Color.WHITE;
        mask[7]=Color.RED;
        mask[8]=Color.BLACK;
        for (int i = 1; i<img.getWidth()-1;++i) {
            for (int j = 1; j<img.getHeight()-1;++j) {
                if(!check(i,j,mask)){
                    temp[i][j]=true;
                }
            }
        }

        mask = new Color[9];
        mask[0]=Color.BLACK;
        mask[1]=Color.BLACK;
        mask[2]=Color.BLACK;
        mask[3]=Color.RED;
        mask[4]=Color.BLACK;
        mask[5]=Color.RED;
        mask[6]=Color.WHITE;
        mask[7]=Color.WHITE;
        mask[8]=Color.WHITE;
        for (int i = 1; i<img.getWidth()-1;++i) {
            for (int j = 1; j<img.getHeight()-1;++j) {
                if(!check(i,j,mask)){
                    temp[i][j]=true;
                }
            }
        }

        mask = new Color[9];
        mask[0]=Color.BLACK;
        mask[1]=Color.RED;
        mask[2]=Color.WHITE;
        mask[3]=Color.BLACK;
        mask[4]=Color.BLACK;
        mask[5]=Color.WHITE;
        mask[6]=Color.BLACK;
        mask[7]=Color.RED;
        mask[8]=Color.WHITE;
        for (int i = 1; i<img.getWidth()-1;++i) {
            for (int j = 1; j<img.getHeight()-1;++j) {
                if(!check(i,j,mask)){
                    temp[i][j]=true;
                }
            }
        }

        mask = new Color[9];
        mask[0]=Color.RED;
        mask[1]=Color.WHITE;
        mask[2]=Color.WHITE;
        mask[3]=Color.BLACK;
        mask[4]=Color.BLACK;
        mask[5]=Color.WHITE;
        mask[6]=Color.RED;
        mask[7]=Color.BLACK;
        mask[8]=Color.RED;
        for (int i = 1; i<img.getWidth()-1;++i) {
            for (int j = 1; j<img.getHeight()-1;++j) {
                if(!check(i,j,mask)){
                    temp[i][j]=true;
                }
            }
        }

        mask = new Color[9];
        mask[0]=Color.WHITE;
        mask[1]=Color.WHITE;
        mask[2]=Color.RED;
        mask[3]=Color.WHITE;
        mask[4]=Color.BLACK;
        mask[5]=Color.BLACK;
        mask[6]=Color.RED;
        mask[7]=Color.BLACK;
        mask[8]=Color.RED;
        for (int i = 1; i<img.getWidth()-1;++i) {
            for (int j = 1; j<img.getHeight()-1;++j) {
                if(!check(i,j,mask)){
                    temp[i][j]=true;
                }
            }
        }

        mask = new Color[9];
        mask[0]=Color.RED;
        mask[1]=Color.BLACK;
        mask[2]=Color.RED;
        mask[3]=Color.WHITE;
        mask[4]=Color.BLACK;
        mask[5]=Color.BLACK;
        mask[6]=Color.WHITE;
        mask[7]=Color.WHITE;
        mask[8]=Color.RED;
        for (int i = 1; i<img.getWidth()-1;++i) {
            for (int j = 1; j<img.getHeight()-1;++j) {
                if(!check(i,j,mask)){
                    temp[i][j]=true;
                }
            }
        }
        mask = new Color[9];
        mask[0]=Color.RED;
        mask[1]=Color.BLACK;
        mask[2]=Color.RED;
        mask[3]=Color.BLACK;
        mask[4]=Color.BLACK;
        mask[5]=Color.WHITE;
        mask[6]=Color.RED;
        mask[7]=Color.WHITE;
        mask[8]=Color.WHITE;
        for (int i = 1; i<img.getWidth()-1;++i) {
            for (int j = 1; j<img.getHeight()-1;++j) {
                if(!check(i,j,mask)){
                    temp[i][j]=true;
                }
            }
        }
        for (int i = 0; i<img.getWidth();++i) {
            for (int j = 0; j<img.getHeight();++j) {
                if(temp[i][j] == true) {
                    img.setRGB(i, j, Color.WHITE.getRGB());
                }
            }
        }
        reloadImage();
    }
    private void duzoPocienianie(){
        int licznik=0;
        int licznik2=0;
        for (int i = 0; i<img.getWidth();++i) {
            for (int j = 0; j<img.getHeight();++j) {
                if(new Color(img.getRGB(i, j)).equals(Color.BLACK)){
                    licznik++;
                }
            }
        }
        for(int i = 0;i<30;++i){
            pocienianie();
        }
        for (int i = 0; i<img.getWidth();++i) {
            for (int j = 0; j<img.getHeight();++j) {
                if(new Color(img.getRGB(i, j)).equals(Color.BLACK)){
                    licznik2++;
                }
            }
        }
        System.out.println("JUSZ");
        System.out.println(licznik);
        System.out.println(licznik2);
    }
    private void saveFile() {
           try{
               File outputfile = new File("pogrubione.jpg");
               ImageIO.write(img, "jpg", outputfile);
           }
           catch(IOException ex) {

           }
    }
    private void duzoPogrubianie(){
        int licznik=0;
        int licznik2=0;
        for (int i = 0; i<img.getWidth();++i) {
            for (int j = 0; j<img.getHeight();++j) {
                if(new Color(img.getRGB(i, j)).equals(Color.BLACK)){
                    licznik++;
                }
            }
        }
        for(int i = 0;i<10;++i){
            pogrubianie();
        }
        for (int i = 0; i<img.getWidth();++i) {
            for (int j = 0; j<img.getHeight();++j) {
                if(new Color(img.getRGB(i, j)).equals(Color.BLACK)){
                    licznik2++;
                }
            }
        }
        System.out.println("JUSZ");
        System.out.println(licznik);
        System.out.println(licznik2);
        saveFile();

    }
    private boolean check(int i, int j, Color []mask){
        boolean temp = false;
        if (!new Color(img.getRGB(i-1, j+1)).equals(mask[0])&&!mask[0].equals(Color.RED)){
            temp = true;
        }
        if (!new Color(img.getRGB(i, j+1)).equals(mask[1])&&!mask[1].equals(Color.RED)){
            temp = true;
        }
        if (!new Color(img.getRGB(i+1, j+1)).equals(mask[2])&&!mask[2].equals(Color.RED)){
            temp = true;
        }
        if (!new Color(img.getRGB(i-1, j)).equals(mask[3])&&!mask[3].equals(Color.RED)){
            temp = true;
        }
        if (!new Color(img.getRGB(i, j)).equals(mask[4])&&!mask[4].equals(Color.RED)){
            temp = true;
        }
        if (!new Color(img.getRGB(i+1, j)).equals(mask[5])&&!mask[5].equals(Color.RED)){
            temp = true;
        }
        if (!new Color(img.getRGB(i-1, j-1)).equals(mask[6])&&!mask[6].equals(Color.RED)){
            temp = true;
        }
        if (!new Color(img.getRGB(i, j-1)).equals(mask[7])&&!mask[7].equals(Color.RED)){
            temp = true;
        }
        if (!new Color(img.getRGB(i+1, j-1)).equals(mask[8])&&!mask[8].equals(Color.RED)){
            temp = true;
        }
//        if(temp==false){
//            System.out.println(i);
//            System.out.println(j);
//        }
        return temp;
    }
    private void erozja() {
        boolean [][]temp = new boolean[img.getWidth()][img.getHeight()];
        for (int i = 1; i<img.getWidth()-1;++i) {
            for (int j = 1; j<img.getHeight()-1;++j) {
                Color c;
                
                if (new Color(img.getRGB(i-1, j-1)).equals(Color.WHITE)){
                    temp[i][j] = true;
                }
                if (new Color(img.getRGB(i-1, j)).equals(Color.WHITE)){
                    temp[i][j] = true;
                }
                if (new Color(img.getRGB(i, j-1)).equals(Color.WHITE)){
                    temp[i][j] = true;
                }
                if (new Color(img.getRGB(i+1, j+1)).equals(Color.WHITE)){
                    temp[i][j] = true;
                }
                if (new Color(img.getRGB(i+1, j)).equals(Color.WHITE)){
                    temp[i][j] = true;
                }
                if (new Color(img.getRGB(i, j+1)).equals(Color.WHITE)){
                    temp[i][j] = true;
                }
                if (new Color(img.getRGB(i+1, j-1)).equals(Color.WHITE)){
                    temp[i][j] = true;
                }
                if (new Color(img.getRGB(i-1, j+1)).equals(Color.WHITE)){
                    temp[i][j] = true;
                }
            }
        }
        for (int i = 0; i<img.getWidth();++i) {
            for (int j = 0; j<img.getHeight();++j) {
                if(temp[i][j] == true) {
                    img.setRGB(i, j, Color.WHITE.getRGB());
                }
            }
        }
        reloadImage();
    }
    
    private void setListeners() {
        backToMain.setOnAction(e -> MainWindow.returnToMainWindow());
        openButton.setOnAction(e -> openFile(e));
        dylatacja.setOnAction(e -> dylatacja());
        erozja.setOnAction(e -> erozja());
        otwarcie.setOnAction(e -> otwarcie());
        zamkniecie.setOnAction(e -> zamkniecie());
        pocienianie.setOnAction(e -> duzoPocienianie());
        pogrubianie.setOnAction(e -> duzoPogrubianie());
    }
    private void reloadImage() {
        Image image = SwingFXUtils.toFXImage(img, null);
        iV.setImage(image);     
    } 
    private void openFile(ActionEvent e) {
        final JFileChooser fc = new JFileChooser("C:\\Users\\blysband\\Downloads\\biom\\JavaFXApplication1");

        int returnVal = fc.showOpenDialog(null);
        if (returnVal == JFileChooser.APPROVE_OPTION) {
            File file = fc.getSelectedFile();
            //This is where a real application would open the file.
            try {
                img = ImageIO.read(new File(file.getName()));
                System.out.println("FlileLoaded");
                reloadImage();
            } catch (IOException ex) {
                System.out.println("FlileNotLoaded");
            }
        } else {
                System.out.println("FlileNotLoaded");
        }
    }
}
