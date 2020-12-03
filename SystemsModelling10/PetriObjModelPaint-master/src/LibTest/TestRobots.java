/*
 * To change this license header, choose License Headers in Project Properties.
 * To change this template file, choose Tools | Templates
 * and open the template in the editor.
 */
package LibTest;

import LibNet.NetLibrary;
import java.util.ArrayList;
import PetriObj.ExceptionInvalidNetStructure;
import PetriObj.ExceptionInvalidTimeDelay;
import PetriObj.PetriObjModel;
import PetriObj.PetriSim;
import PetriObj.PetriT;
import PetriObj.PetriP;
import PetriObj.ArcIn;
import PetriObj.ArcOut;
import PetriObj.PetriNet;

/**
 *
 * @author Oleksandra_Vozniuk
 */
public class TestRobots {
    public static void main(String[] args) throws ExceptionInvalidTimeDelay, ExceptionInvalidNetStructure {
          // цей фрагмент для запуску імітації моделі з заданною мережею Петрі на інтервалі часу timeModeling  
          PetriObjModel model = getModel1();
          model.setIsProtokol(false);
          double timeModeling = 10000;
          model.go(timeModeling);
          
                System.out.println("-------------------------------------------------------");
                System.out.println("Статистика:");
                System.out.println("К-ть деталей, що надійшли на обробку");
		System.out.println(model.getListObj().get(0).getNet().getListP()[2].getMark());
		System.out.println("К-ть оброблених деталей");
		System.out.println(model.getListObj().get(5).getNet().getListP()[3].getMark());
		System.out.println("Середня к-ть у черзі до першого верстату");
		System.out.println(model.getListObj().get(2).getNet().getListP()[1].getMean());
		System.out.println("Середня к-ть у черзі до другого верстату");
		System.out.println(model.getListObj().get(4).getNet().getListP()[1].getMean());
		System.out.println("Відсоток оброблених деталей");
		double per = (double) model.getListObj().get(5).getNet().getListP()[3].getMark()
				/ (double) model.getListObj().get(0).getNet().getListP()[2].getMark() * 100;
		System.out.println(per + "%");
                
                PetriObjModel model1 = getModel2();
                model1.setIsProtokol(false);
                model1.go(timeModeling);
                
                System.out.println("-------------------------------------------------------");
                System.out.println("Статистика 2:");
		System.out.println("К-ть деталей, що надійшли на обробку");
		System.out.println(model1.getListObj().get(0).getNet().getListP()[2].getMark());
		System.out.println("К-ть оброблених деталей");
		System.out.println(model1.getListObj().get(9).getNet().getListP()[3].getMark());
		System.out.println("Середня к-ть у черзі до першого верстату");
		System.out.println(model1.getListObj().get(2).getNet().getListP()[1].getMean());
		System.out.println("Середня к-ть у черзі до другого верстату");
		System.out.println(model1.getListObj().get(4).getNet().getListP()[1].getMean());
		System.out.println("Середня к-ть у черзі до третього верстату");
		System.out.println(model1.getListObj().get(6).getNet().getListP()[1].getMean());
		System.out.println("Середня к-ть у черзі до чнтвертого верстату");
		System.out.println(model1.getListObj().get(8).getNet().getListP()[1].getMean());
		System.out.println("Відсоток оброблених деталей");
		double per2 = (double) model1.getListObj().get(9).getNet().getListP()[3].getMark()
				/ (double) model1.getListObj().get(0).getNet().getListP()[2].getMark() * 100;
		System.out.println(per2 + "%");
          }
          public static PetriObjModel getModel1() throws ExceptionInvalidTimeDelay, ExceptionInvalidNetStructure {
		ArrayList<PetriSim> list = new ArrayList<PetriSim>();

		list.add(new PetriSim(CreateIncoming())); //надходження деталей
		list.add(new PetriSim(CreateAction(6.0, 1))); //переміщення робота з пункту прибуття на перший верстат
								
		list.add(new PetriSim(CreateProcessing(41))); //обробка першим пристроєм
		list.add(new PetriSim(CreateAction(7.0, 1))); //переміщення робота з першого верстату на другий верстат
																		
		list.add(new PetriSim(CreateProcessing(42))); //обробка другим пристроєм
		list.add(new PetriSim(CreateAction(5.0, 1))); //переміщення робота з другого верстату на склад
																			

		list.get(0).getNet().getListP()[1] = list.get(1).getNet().getListP()[0]; 
		list.get(1).getNet().getListP()[3] = list.get(2).getNet().getListP()[0]; 
		list.get(2).getNet().getListP()[2] = list.get(3).getNet().getListP()[0]; 
		list.get(3).getNet().getListP()[3] = list.get(4).getNet().getListP()[0]; 
		list.get(4).getNet().getListP()[2] = list.get(5).getNet().getListP()[0]; 
		PetriObjModel model = new PetriObjModel(list);
		return model;
	}
 
          public static PetriObjModel getModel2() throws ExceptionInvalidTimeDelay, ExceptionInvalidNetStructure {
		ArrayList<PetriSim> list = new ArrayList<PetriSim>();

		list.add(new PetriSim(CreateIncoming())); //надходження деталей
		list.add(new PetriSim(CreateAction(6.0, 3))); //переміщення робота з пункту прибуття на перший верстат
								
		list.add(new PetriSim(CreateProcessing(41))); //обробка першим пристроєм
		list.add(new PetriSim(CreateAction(7.0, 0))); //переміщення робота з першого верстату на другий верстат
																		
		list.add(new PetriSim(CreateProcessing(42))); //обробка другим пристроєм
		list.add(new PetriSim(CreateAction(6.0, 0))); //переміщення робота з другого верстату на третій верстат
								
		list.add(new PetriSim(CreateProcessing(43))); //обробка третім пристроєм
		list.add(new PetriSim(CreateAction(7.0, 0))); //переміщення робота з третього верстату на четвертий верстат
																		
		list.add(new PetriSim(CreateProcessing(44))); //обробка другим пристроєм
		list.add(new PetriSim(CreateAction(5.0, 0))); //переміщення робота з четвертого верстату на склад
					

		list.get(0).getNet().getListP()[1] = list.get(1).getNet().getListP()[0]; 
		list.get(1).getNet().getListP()[3] = list.get(2).getNet().getListP()[0]; 
		list.get(2).getNet().getListP()[2] = list.get(3).getNet().getListP()[0]; 
		list.get(3).getNet().getListP()[3] = list.get(4).getNet().getListP()[0]; 
		list.get(4).getNet().getListP()[2] = list.get(5).getNet().getListP()[0]; 
		list.get(5).getNet().getListP()[3] = list.get(6).getNet().getListP()[0]; 
		list.get(6).getNet().getListP()[2] = list.get(7).getNet().getListP()[0]; 
		list.get(7).getNet().getListP()[3] = list.get(8).getNet().getListP()[0]; 
		list.get(8).getNet().getListP()[2] = list.get(9).getNet().getListP()[0]; 
		PetriObjModel model = new PetriObjModel(list);
		return model;
	}
public static PetriNet CreateIncoming() throws ExceptionInvalidNetStructure, ExceptionInvalidTimeDelay {
                ArrayList<PetriP> d_P = new ArrayList<>();
		ArrayList<PetriT> d_T = new ArrayList<>();
		ArrayList<ArcIn> d_In = new ArrayList<>();
		ArrayList<ArcOut> d_Out = new ArrayList<>();
		d_P.add(new PetriP("P1", 1));
		d_P.add(new PetriP("P2", 0));
		d_P.add(new PetriP("P3", 0));
		d_T.add(new PetriT("T1", 40.0));
		d_T.get(0).setDistribution("exp", d_T.get(0).getTimeServ());
		d_T.get(0).setParamDeviation(0.0);
		d_In.add(new ArcIn(d_P.get(0), d_T.get(0), 1));
		d_Out.add(new ArcOut(d_T.get(0), d_P.get(0), 1));
		d_Out.add(new ArcOut(d_T.get(0), d_P.get(1), 1));
		d_Out.add(new ArcOut(d_T.get(0), d_P.get(2), 1));
		PetriNet d_Net = new PetriNet("Incoming", d_P, d_T, d_In, d_Out);
		PetriP.initNext();
		PetriT.initNext();
		ArcIn.initNext();
		ArcOut.initNext();

		return d_Net;
}

public static PetriNet CreateAction(double delayMean, int amountOfRobots) throws ExceptionInvalidNetStructure, ExceptionInvalidTimeDelay {
	ArrayList<PetriP> d_P = new ArrayList<>();
	ArrayList<PetriT> d_T = new ArrayList<>();
	ArrayList<ArcIn> d_In = new ArrayList<>();
	ArrayList<ArcOut> d_Out = new ArrayList<>();
	d_P.add(new PetriP("P2",0));
	d_P.add(new PetriP("P3",0));
	d_P.add(new PetriP("P4",0));
	d_P.add(new PetriP("P5",0));
	d_P.add(new PetriP("P16",1));
	d_T.add(new PetriT("Захоплення",8.0));
	d_T.add(new PetriT("Переміщення до верстату",0.0));
	d_T.add(new PetriT("Вивільнення",8.0));
	d_In.add(new ArcIn(d_P.get(0),d_T.get(0),1));
	d_In.add(new ArcIn(d_P.get(1),d_T.get(1),1));
	d_In.add(new ArcIn(d_P.get(2),d_T.get(2),1));
	d_In.add(new ArcIn(d_P.get(4),d_T.get(0),1));
	d_Out.add(new ArcOut(d_T.get(0),d_P.get(1),1));
	d_Out.add(new ArcOut(d_T.get(1),d_P.get(2),1));
	d_Out.add(new ArcOut(d_T.get(2),d_P.get(3),1));
	d_Out.add(new ArcOut(d_T.get(2),d_P.get(4),1));
	PetriNet d_Net = new PetriNet("t2",d_P,d_T,d_In,d_Out);
	PetriP.initNext();
	PetriT.initNext();
	ArcIn.initNext();
	ArcOut.initNext();

	return d_Net;
}

public static PetriNet CreateProcessing(double delayMean) throws ExceptionInvalidNetStructure, ExceptionInvalidTimeDelay {
	ArrayList<PetriP> d_P = new ArrayList<>();
	ArrayList<PetriT> d_T = new ArrayList<>();
	ArrayList<ArcIn> d_In = new ArrayList<>();
	ArrayList<ArcOut> d_Out = new ArrayList<>();
	d_P.add(new PetriP("P5",0));
	d_P.add(new PetriP("P6",3));
	d_P.add(new PetriP("P7",0));
	d_T.add(new PetriT("Processing",60.0));
	d_In.add(new ArcIn(d_P.get(0),d_T.get(0),1));
	d_In.add(new ArcIn(d_P.get(1),d_T.get(0),1));
	d_Out.add(new ArcOut(d_T.get(0),d_P.get(1),1));
	d_Out.add(new ArcOut(d_T.get(0),d_P.get(2),1));
	PetriNet d_Net = new PetriNet("2",d_P,d_T,d_In,d_Out);
	PetriP.initNext();
	PetriT.initNext();
	ArcIn.initNext();
	ArcOut.initNext();

	return d_Net;
    }
}
