/*
 * To change this license header, choose License Headers in Project Properties.
 * To change this template file, choose Tools | Templates
 * and open the template in the editor.
 */
package LibTest;

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

/**
 *
 * @author Oleksandra_Vozniuk
 */
public class TestBuses {
     public static void main(String[] args) throws ExceptionInvalidTimeDelay, ExceptionInvalidNetStructure {
          // цей фрагмент для запуску імітації моделі з заданною мережею Петрі на інтервалі часу timeModeling  
          PetriObjModel model = getModel1();
          model.setIsProtokol(false);
          double timeModeling = 100000;
          model.go(timeModeling);
          
                              System.out.println("-------------------------------------------------------");
                System.out.println("Статистичні результати:");
                System.out.println("Прибуток");
		System.out.println(model.getListObj().get(2).getNet().getListP()[3].getMark()+model.getListObj().get(3).getNet().getListP()[3].getMark());
		System.out.println("Середня к-ть клієнтів в черзі");
		System.out.println(model.getListObj().get(1).getNet().getListP()[1].getMean());
		System.out.println("Втрати");
		System.out.println(model.getListObj().get(1).getNet().getListP()[2].getMark());
                
          model = getModel2();
          model.setIsProtokol(false);
          model.go(timeModeling);
          
                              System.out.println("-------------------------------------------------------");
                System.out.println("Статистичні результати 2:");
                System.out.println("Прибуток");
		System.out.println(
                        model.getListObj().get(2).getNet().getListP()[3].getMark()
                        +model.getListObj().get(3).getNet().getListP()[3].getMark()
                        +model.getListObj().get(4).getNet().getListP()[3].getMark()
                        +model.getListObj().get(5).getNet().getListP()[3].getMark()
                        +model.getListObj().get(6).getNet().getListP()[3].getMark()
                        +model.getListObj().get(7).getNet().getListP()[3].getMark()
                        +model.getListObj().get(8).getNet().getListP()[3].getMark()
                        +model.getListObj().get(9).getNet().getListP()[3].getMark()
                        +model.getListObj().get(10).getNet().getListP()[3].getMark()
                        +model.getListObj().get(11).getNet().getListP()[3].getMark());
		System.out.println("Середня к-ть клієнтів в черзі");
		System.out.println(model.getListObj().get(1).getNet().getListP()[1].getMean());
		System.out.println("Втрати");
		System.out.println(model.getListObj().get(1).getNet().getListP()[2].getMark());

          }
          public static PetriObjModel getModel1() throws ExceptionInvalidTimeDelay, ExceptionInvalidNetStructure {
		ArrayList<PetriSim> list = new ArrayList<PetriSim>();

		list.add(new PetriSim(CreateIncoming()));
                
                list.add(new PetriSim(CreateBoarding()));
								
		list.add(new PetriSim(CreateTransfer()));
		list.add(new PetriSim(CreateTransfer()));

		list.get(0).getNet().getListP()[1] = list.get(1).getNet().getListP()[0]; 
		list.get(1).getNet().getListP()[1] = list.get(2).getNet().getListP()[0]; 
		list.get(1).getNet().getListP()[1] = list.get(3).getNet().getListP()[0]; 
		PetriObjModel model = new PetriObjModel(list);
		return model;
	}
 
          public static PetriObjModel getModel2() throws ExceptionInvalidTimeDelay, ExceptionInvalidNetStructure {
              ArrayList<PetriSim> list = new ArrayList<PetriSim>();

		list.add(new PetriSim(CreateIncoming()));
                
                list.add(new PetriSim(CreateBoarding()));
								
		list.add(new PetriSim(CreateTransfer()));
		list.add(new PetriSim(CreateTransfer()));
                list.add(new PetriSim(CreateTransfer()));
		list.add(new PetriSim(CreateTransfer()));
                list.add(new PetriSim(CreateTransfer()));
		list.add(new PetriSim(CreateTransfer()));
                list.add(new PetriSim(CreateTransfer()));
		list.add(new PetriSim(CreateTransfer()));
                list.add(new PetriSim(CreateTransfer()));
		list.add(new PetriSim(CreateTransfer()));

		list.get(0).getNet().getListP()[1] = list.get(1).getNet().getListP()[0]; 
		list.get(1).getNet().getListP()[1] = list.get(2).getNet().getListP()[0]; 
		list.get(1).getNet().getListP()[1] = list.get(3).getNet().getListP()[0]; 
                list.get(1).getNet().getListP()[1] = list.get(4).getNet().getListP()[0]; 
		list.get(1).getNet().getListP()[1] = list.get(5).getNet().getListP()[0]; 
                list.get(1).getNet().getListP()[1] = list.get(6).getNet().getListP()[0]; 
		list.get(1).getNet().getListP()[1] = list.get(7).getNet().getListP()[0]; 
                list.get(1).getNet().getListP()[1] = list.get(8).getNet().getListP()[0]; 
		list.get(1).getNet().getListP()[1] = list.get(9).getNet().getListP()[0]; 
                list.get(1).getNet().getListP()[1] = list.get(10).getNet().getListP()[0];
                list.get(1).getNet().getListP()[1] = list.get(11).getNet().getListP()[0]; 
		PetriObjModel model = new PetriObjModel(list);
		return model;
	}
public static PetriNet CreateIncoming() throws ExceptionInvalidNetStructure, ExceptionInvalidTimeDelay {
              ArrayList<PetriP> d_P = new ArrayList<>();
	ArrayList<PetriT> d_T = new ArrayList<>();
	ArrayList<ArcIn> d_In = new ArrayList<>();
	ArrayList<ArcOut> d_Out = new ArrayList<>();
	d_P.add(new PetriP("P1",1));
	d_P.add(new PetriP("P2",0));
	d_T.add(new PetriT("Incoming",0.05));
	d_In.add(new ArcIn(d_P.get(0),d_T.get(0),1));
	d_Out.add(new ArcOut(d_T.get(0),d_P.get(0),1));
	d_Out.add(new ArcOut(d_T.get(0),d_P.get(1),1));
	PetriNet d_Net = new PetriNet("incoming_1",d_P,d_T,d_In,d_Out);
	PetriP.initNext();
	PetriT.initNext();
	ArcIn.initNext();
	ArcOut.initNext();

	return d_Net;
}

public static PetriNet CreateBoarding() throws ExceptionInvalidNetStructure, ExceptionInvalidTimeDelay {
	ArrayList<PetriP> d_P = new ArrayList<>();
	ArrayList<PetriT> d_T = new ArrayList<>();
	ArrayList<ArcIn> d_In = new ArrayList<>();
	ArrayList<ArcOut> d_Out = new ArrayList<>();
	d_P.add(new PetriP("P1",0));
	d_P.add(new PetriP("P2",0));
	d_P.add(new PetriP("ExitCount",0));
	d_T.add(new PetriT("Incoming",0.0));
	d_T.add(new PetriT("Exit",0.0));
	d_T.get(1).setPriority(1);
	d_In.add(new ArcIn(d_P.get(0),d_T.get(0),1));
	d_In.add(new ArcIn(d_P.get(1),d_T.get(1),30));
	d_In.get(1).setInf(true);
	d_In.add(new ArcIn(d_P.get(0),d_T.get(1),1));
	d_Out.add(new ArcOut(d_T.get(0),d_P.get(1),1));
	d_Out.add(new ArcOut(d_T.get(1),d_P.get(2),1));
	PetriNet d_Net = new PetriNet("incoming_1",d_P,d_T,d_In,d_Out);
	PetriP.initNext();
	PetriT.initNext();
	ArcIn.initNext();
	ArcOut.initNext();

	return d_Net;
}

public static PetriNet CreateTransfer() throws ExceptionInvalidNetStructure, ExceptionInvalidTimeDelay {
	ArrayList<PetriP> d_P = new ArrayList<>();
	ArrayList<PetriT> d_T = new ArrayList<>();
	ArrayList<ArcIn> d_In = new ArrayList<>();
	ArrayList<ArcOut> d_Out = new ArrayList<>();
	d_P.add(new PetriP("P1",0));
	d_P.add(new PetriP("P2",0));
	d_P.add(new PetriP("P2",0));
	d_P.add(new PetriP("P3",0));
	d_P.add(new PetriP("P4",1));
	d_T.add(new PetriT("Boarding",0.0));
	d_T.add(new PetriT("T3",20.0));
	d_T.get(1).setDistribution("unif", d_T.get(1).getTimeServ());
	d_T.get(1).setParamDeviation(10.0);
	d_T.add(new PetriT("T4",5.0));
	d_T.get(2).setDistribution("unif", d_T.get(2).getTimeServ());
	d_T.get(2).setParamDeviation(1.0);
	d_In.add(new ArcIn(d_P.get(0),d_T.get(0),1));
	d_In.add(new ArcIn(d_P.get(1),d_T.get(1),1));
	d_In.add(new ArcIn(d_P.get(2),d_T.get(2),1));
	d_In.add(new ArcIn(d_P.get(4),d_T.get(0),1));
	d_Out.add(new ArcOut(d_T.get(0),d_P.get(1),1));
	d_Out.add(new ArcOut(d_T.get(1),d_P.get(2),1));
	d_Out.add(new ArcOut(d_T.get(2),d_P.get(3),1));
	d_Out.add(new ArcOut(d_T.get(2),d_P.get(4),1));
	PetriNet d_Net = new PetriNet("incoming_1",d_P,d_T,d_In,d_Out);
	PetriP.initNext();
	PetriT.initNext();
	ArcIn.initNext();
	ArcOut.initNext();

	return d_Net;
}


}
