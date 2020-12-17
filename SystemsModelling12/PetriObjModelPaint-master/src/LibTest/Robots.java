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
public class Robots {
    
     public static void main(String[] args) throws ExceptionInvalidTimeDelay, ExceptionInvalidNetStructure {
          // цей фрагмент для запуску імітації моделі з заданною мережею Петрі на інтервалі часу timeModeling  
          PetriNet robots = CreateRobots();
          ArrayList<PetriSim> list = new ArrayList<PetriSim>();
          list.add(new PetriSim(CreateRobots()));
          PetriObjModel model = new PetriObjModel(list);

          model.setIsProtokol(false);
          double timeModeling = 10000;
          

          System.out.println(model.go(timeModeling,0.001));

          }
    
    public static PetriNet CreateRobots() throws ExceptionInvalidNetStructure, ExceptionInvalidTimeDelay {
	ArrayList<PetriP> d_P = new ArrayList<>();
	ArrayList<PetriT> d_T = new ArrayList<>();
	ArrayList<ArcIn> d_In = new ArrayList<>();
	ArrayList<ArcOut> d_Out = new ArrayList<>();
	d_P.add(new PetriP("P1",1));
	d_P.add(new PetriP("P2",0));
	d_P.add(new PetriP("P3",0));
	d_P.add(new PetriP("P4",0));
	d_P.add(new PetriP("P5",0));
	d_P.add(new PetriP("P6",3));
	d_P.add(new PetriP("P7",0));
	d_P.add(new PetriP("P8",0));
	d_P.add(new PetriP("P9",0));
	d_P.add(new PetriP("P10",0));
	d_P.add(new PetriP("P11",3));
	d_P.add(new PetriP("P12",0));
	d_P.add(new PetriP("P13",0));
	d_P.add(new PetriP("P14",0));
	d_P.add(new PetriP("P15",0));
	d_P.add(new PetriP("P16",1));
	d_P.add(new PetriP("P17",1));
	d_P.add(new PetriP("P18",1));
	d_T.add(new PetriT("Надходження",40.0));
	d_T.get(0).setDistribution("exp", d_T.get(0).getTimeServ());
	d_T.get(0).setParamDeviation(0.0);
	d_T.add(new PetriT("Захоплення",8.0));
	d_T.get(1).setDistribution("unif", d_T.get(1).getTimeServ());
	d_T.get(1).setParamDeviation(1.0);
	d_T.add(new PetriT("Переміщення до верстату",6.0));
	d_T.add(new PetriT("Вивільнення",8.0));
	d_T.get(3).setDistribution("unif", d_T.get(3).getTimeServ());
	d_T.get(3).setParamDeviation(1.0);
	d_T.add(new PetriT("Обробка верстатом 1",60.0));
	d_T.get(4).setDistribution("norm", d_T.get(4).getTimeServ());
	d_T.get(4).setParamDeviation(10.0);
	d_T.add(new PetriT("Захоплення 2",8.0));
	d_T.get(5).setDistribution("unif", d_T.get(5).getTimeServ());
	d_T.get(5).setParamDeviation(1.0);
	d_T.add(new PetriT("Переміщення до верстату 2",6.0));
	d_T.add(new PetriT("Вивільнення 2",8.0));
	d_T.get(7).setDistribution("unif", d_T.get(7).getTimeServ());
	d_T.get(7).setParamDeviation(1.0);
	d_T.add(new PetriT("Обробка верстатом 2",60.0));
	d_T.get(8).setDistribution("norm", d_T.get(8).getTimeServ());
	d_T.get(8).setParamDeviation(10.0);
	d_T.add(new PetriT("Захоплення 3",8.0));
	d_T.get(9).setDistribution("unif", d_T.get(9).getTimeServ());
	d_T.get(9).setParamDeviation(1.0);
	d_T.add(new PetriT("Переміщення на склад",6.0));
	d_T.add(new PetriT("Вивільнення робота",8.0));
	d_T.get(11).setDistribution("unif", d_T.get(11).getTimeServ());
	d_T.get(11).setParamDeviation(1.0);
	d_In.add(new ArcIn(d_P.get(1),d_T.get(1),1));
	d_In.add(new ArcIn(d_P.get(15),d_T.get(1),1));
	d_In.add(new ArcIn(d_P.get(4),d_T.get(4),1));
	d_In.add(new ArcIn(d_P.get(5),d_T.get(4),1));
	d_In.add(new ArcIn(d_P.get(8),d_T.get(7),1));
	d_In.add(new ArcIn(d_P.get(3),d_T.get(3),1));
	d_In.add(new ArcIn(d_P.get(12),d_T.get(10),1));
	d_In.add(new ArcIn(d_P.get(11),d_T.get(9),1));
	d_In.add(new ArcIn(d_P.get(17),d_T.get(9),1));
	d_In.add(new ArcIn(d_P.get(13),d_T.get(11),1));
	d_In.add(new ArcIn(d_P.get(9),d_T.get(8),1));
	d_In.add(new ArcIn(d_P.get(10),d_T.get(8),1));
	d_In.add(new ArcIn(d_P.get(0),d_T.get(0),1));
	d_In.add(new ArcIn(d_P.get(2),d_T.get(2),1));
	d_In.add(new ArcIn(d_P.get(6),d_T.get(5),1));
	d_In.add(new ArcIn(d_P.get(16),d_T.get(5),1));
	d_In.add(new ArcIn(d_P.get(7),d_T.get(6),1));
	d_Out.add(new ArcOut(d_T.get(1),d_P.get(2),1));
	d_Out.add(new ArcOut(d_T.get(4),d_P.get(5),1));
	d_Out.add(new ArcOut(d_T.get(4),d_P.get(6),1));
	d_Out.add(new ArcOut(d_T.get(7),d_P.get(9),1));
	d_Out.add(new ArcOut(d_T.get(7),d_P.get(16),1));
	d_Out.add(new ArcOut(d_T.get(3),d_P.get(4),1));
	d_Out.add(new ArcOut(d_T.get(3),d_P.get(15),1));
	d_Out.add(new ArcOut(d_T.get(10),d_P.get(13),1));
	d_Out.add(new ArcOut(d_T.get(9),d_P.get(12),1));
	d_Out.add(new ArcOut(d_T.get(11),d_P.get(14),1));
	d_Out.add(new ArcOut(d_T.get(11),d_P.get(17),1));
	d_Out.add(new ArcOut(d_T.get(8),d_P.get(11),1));
	d_Out.add(new ArcOut(d_T.get(8),d_P.get(10),1));
	d_Out.add(new ArcOut(d_T.get(0),d_P.get(0),1));
	d_Out.add(new ArcOut(d_T.get(0),d_P.get(1),1));
	d_Out.add(new ArcOut(d_T.get(2),d_P.get(3),1));
	d_Out.add(new ArcOut(d_T.get(5),d_P.get(7),1));
	d_Out.add(new ArcOut(d_T.get(6),d_P.get(8),1));
	PetriNet d_Net = new PetriNet("task2_graph",d_P,d_T,d_In,d_Out);
	PetriP.initNext();
	PetriT.initNext();
	ArcIn.initNext();
	ArcOut.initNext();

	return d_Net;
}
}
