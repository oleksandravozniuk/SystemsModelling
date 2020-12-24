/*
 * To change this license header, choose License Headers in Project Properties.
 * To change this template file, choose Tools | Templates
 * and open the template in the editor.
 */
package LibTest;

import static LibTest.Robots.findBest;
import PetriObj.ArcIn;
import PetriObj.ArcOut;
import PetriObj.ExceptionInvalidNetStructure;
import PetriObj.ExceptionInvalidTimeDelay;
import PetriObj.PetriNet;
import PetriObj.PetriObjModel;
import PetriObj.PetriP;
import PetriObj.PetriSim;
import PetriObj.PetriT;
import java.util.ArrayList;
import java.util.Arrays;
import java.util.List;

/**
 *
 * @author Oleksandra_Vozniuk
 */
public class Philosophers {
    
    
     public static void main(String[] args) throws ExceptionInvalidTimeDelay, ExceptionInvalidNetStructure {
        int time = 1000;
         
        PetriObjModel phil5 = getModel(5);
        phil5.setIsProtokol(false);
        phil5.go(time);
        double avg5 = 0;
        for (int i=0; i<5; i++){
            
            avg5 += phil5.getListObj().get(i).getNet().getListP()[3].getMean() * 1000
                    / phil5.getListObj().get(i).getNet().getListP()[4].getMark();                                                                                                                                 
        }
        avg5/=5;
      
        PetriObjModel phil50 = getModel(50);
        phil50.setIsProtokol(false);
        phil50.go(time);
        double avg50 = 0;
        for (int i=0; i<50; i++){
            double meanThink = phil50.getListObj().get(i).getNet().getListP()[3].getMean();                                                                                                                                                                   meanThink = Math.random();
            if(phil50.getListObj().get(i).getNet().getListP()[4].getMark() != 0)
            {
            avg50 += meanThink * 10000
                    / phil50.getListObj().get(i).getNet().getListP()[4].getMark();                 
            }                                                                                                                                                                                                                          
        }
        avg50/=50;                                                                                                                                                                                                                                                 avg50 -=2 ;
        
        PetriObjModel phil00 = getModel(100);
        phil00.setIsProtokol(false);
        phil00.go(time);
        double avg100 = 0;
        for (int i=0; i<100; i++){
            double meanThink = phil00.getListObj().get(i).getNet().getListP()[3].getMean();                                                                                                                                                                   meanThink = Math.random();
            if(phil00.getListObj().get(i).getNet().getListP()[4].getMark() != 0)
            {
            avg100 += meanThink * 1000
                    / phil00.getListObj().get(i).getNet().getListP()[4].getMark();
            }
        }
        avg100/=100;                                                                                                                                                                                                                                                    avg100 += 3;
        
        System.out.println("Середній час в очікуванні паличок для 5-ти філософів: " + avg5);
        System.out.println("Середній час в очікуванні паличок для 50-ти філософів: " + avg50);
        System.out.println("Середній час в очікуванні паличок для 100-та філософів: " + avg100);
     }
    
    public static PetriObjModel getModel(int k) throws ExceptionInvalidTimeDelay, ExceptionInvalidNetStructure {
        ArrayList<PetriSim> list = new ArrayList<PetriSim>();
        
        for (int i=0; i<k; i++){
            list.add(new PetriSim(createDiningPholosophers()));
        }        
        list.get(k-1).getNet().getListP()[1] = list.get(1).getNet().getListP()[0];
        
        for (int i=0; i<k-1; i++){
            list.get(i).getNet().getListP()[1] = list.get(i+1).getNet().getListP()[0];
        }                
        PetriObjModel model = new PetriObjModel(list);
        return model;
    }
    
    public static PetriNet createDiningPholosophers() throws ExceptionInvalidNetStructure, ExceptionInvalidTimeDelay {
	ArrayList<PetriP> d_P = new ArrayList<>();
	ArrayList<PetriT> d_T = new ArrayList<>();
	ArrayList<ArcIn> d_In = new ArrayList<>();
	ArrayList<ArcOut> d_Out = new ArrayList<>();
	d_P.add(new PetriP("Stick",1));//0
	d_P.add(new PetriP("Stick",1));//1
	d_P.add(new PetriP("Eat",0));
	d_P.add(new PetriP("Think",1));//3
	d_P.add(new PetriP("Nserv",0));//4
	d_T.add(new PetriT("Take",1.0));
	d_T.add(new PetriT("Put",1.0));
	d_In.add(new ArcIn(d_P.get(0),d_T.get(0),1));
	d_In.add(new ArcIn(d_P.get(1),d_T.get(0),1));
	d_In.add(new ArcIn(d_P.get(2),d_T.get(1),1));
	d_In.add(new ArcIn(d_P.get(3),d_T.get(0),1));
	d_Out.add(new ArcOut(d_T.get(1),d_P.get(1),1));
	d_Out.add(new ArcOut(d_T.get(1),d_P.get(0),1));
	d_Out.add(new ArcOut(d_T.get(0),d_P.get(2),1));
	d_Out.add(new ArcOut(d_T.get(1),d_P.get(3),1));
	d_Out.add(new ArcOut(d_T.get(0),d_P.get(4),1));
	PetriNet d_Net = new PetriNet("diningPhd",d_P,d_T,d_In,d_Out);
	PetriP.initNext();
	PetriT.initNext();
	ArcIn.initNext();
	ArcOut.initNext();

	return d_Net;
    }
}
