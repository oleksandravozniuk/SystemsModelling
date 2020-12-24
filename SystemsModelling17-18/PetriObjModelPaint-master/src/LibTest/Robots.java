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
import java.util.Arrays;
import java.util.List;

/**
 *
 * @author Oleksandra_Vozniuk
 */
public class Robots {
    
     public static void main(String[] args) throws ExceptionInvalidTimeDelay, ExceptionInvalidNetStructure {
          int timeModelling = 150000;
          List<Integer> verstat1 = Arrays.asList(1,2,3,5,1,4);
          List<Integer> verstat2 = Arrays.asList(7,6,5,1,7,2);
          List<Integer> robot1 = Arrays.asList(3,1,2,6,3,2);
          List<Integer> robot2 = Arrays.asList(5,3,2,5,5,1);
          List<Integer> robot3 = Arrays.asList(4,3,1,1,2,3);
          double epsilon = 0.001;
          double currentValue = Double.MAX_VALUE;
          double prevValue = 0;
          int n = 6;
          int iter = 0;
       
          while(Math.abs(currentValue - prevValue) > epsilon)
          {
              iter++;
              System.out.println("Iteration " + iter);
              //Пошук показниців життєдіяльності та визначення найліпшого значення
              double[] result = findBest(timeModelling,n, verstat1,verstat2,robot1,robot2,robot3);
              System.out.println("Productivity: " + result[(int)result[0] + 1]);
               if(iter==1)
              {prevValue = 0;}
              else
              {prevValue = currentValue;}
              
              currentValue = result[(int)result[0] + 1];
              System.out.println("Verstat1: " + verstat1.get((int)result[0]));
              System.out.println("Verstat2: " + verstat2.get((int)result[0]));
              System.out.println("Robot1: " + robot1.get((int)result[0]));
              System.out.println("Robot2: " + robot2.get((int)result[0]));
              System.out.println("Robot3: " + robot3.get((int)result[0]));
              
              //відкидання половини найгірших
              ArrayList<Integer> indexesToRemove = new ArrayList<Integer>();
              for(int i = 0;i<(n/2.0);i++)
              {
                  int indexToRemove = 0;
                  double valueToRemove = result[1];
                  for(int j = 0;j<n;j++)
                  {
                      if(result[j+1]<valueToRemove)
                      {
                          valueToRemove = result[j+1];
                          indexToRemove = j;
                      }
                  }
                 indexesToRemove.add(indexToRemove);
                  result[indexToRemove+1] = Integer.MAX_VALUE;
              }
             
                  System.out.println(prevValue);
                  System.out.println(currentValue);
              //кросинговер
               List<Integer> verstat11 = new ArrayList<Integer>();
                List<Integer> verstat22 = new ArrayList<Integer>();
                List<Integer> robot11 = new ArrayList<Integer>();
                List<Integer> robot22 = new ArrayList<Integer>();
                List<Integer> robot33 = new ArrayList<Integer>();
                for(int i=0;i<n;i++)
                {
                    Boolean flag = true;
                    for(int j=0;j<indexesToRemove.size();j++)
                    {
                        if(i==indexesToRemove.get(j))
                        {
                            flag = false;
                        }
                    }
                    if(!flag)
                    {
                        verstat11.add(verstat1.get(i));
                        verstat22.add(verstat2.get(i));
                        robot11.add(robot1.get(i));
                        robot22.add(robot2.get(i));
                        robot33.add(robot3.get(i));
                    }

                }
              for(int i = 0;i<n/2;i++)
              {
                  if(i==((n/2)-1))
                  {
                  verstat11.add(verstat1.get(i));
                  verstat22.add(verstat2.get(i));
                  robot11.add(robot1.get(0));
                  robot22.add(robot2.get(0));
                  robot33.add(robot3.get(0));
                  }
                  else
                  {
                  verstat11.add(verstat1.get(i));
                  verstat22.add(verstat2.get(i));
                  robot11.add(robot1.get(i+1));
                  robot22.add(robot2.get(i+1));
                  robot33.add(robot3.get(i+1));
                  }
              }

              //мутація
                for(int i=0;i<n;i++)
                {
                    int a = 0; // Начальное значение диапазона - "от"
                    int b = 100; // Конечное значение диапазона - "до"
                    int probability = a + (int) (Math.random() * b); // Генерация 1-го числа
                    
                    if(probability>=50)
                    {
                        int c = 0; // Начальное значение диапазона - "от"
                        int k = 4; // Конечное значение диапазона - "до"
                        int prob = c + (int) (Math.random() * k); // Генерация 1-го числа
                        
                        int q = 0; // Начальное значение диапазона - "от"
                        int w = 1; // Конечное значение диапазона - "до"
                        int mutationType = q + (int) (Math.random() * w); // Генерация 1-го числа
                        
                        int mutationValue = 2;
                        if(mutationType==1)
                        {
                            mutationValue *=-1; 
                        }
                        
                        switch(prob){
                            case 0: 
                                if(verstat11.get(i)>1)
                                {verstat11.set(i, verstat11.get(i)+mutationValue);}
                                break;
                            case 1: 
                                if(verstat22.get(i)>1)
                                {verstat22.set(i, verstat22.get(i)+mutationValue);}
                                break;
                            case 2: 
                                if(robot11.get(i)>1)
                                {robot11.set(i, robot11.get(i)+mutationValue);}
                                break;
                            case 3: 
                                if(robot22.get(i)>1)
                                {robot22.set(i, robot22.get(i)+mutationValue);}
                                break;
                            case 4: 
                                if(robot33.get(i)>1)
                                {robot33.set(i, robot33.get(i)+mutationValue);}
                                break;
                        }
                    }
                }
                
                //створити нове покоління
                List<Integer> verstat111 = new ArrayList<Integer>();
                List<Integer> verstat222 = new ArrayList<Integer>();
                List<Integer> robot111 = new ArrayList<Integer>();
                List<Integer> robot222 = new ArrayList<Integer>();
                List<Integer> robot333 = new ArrayList<Integer>();
                for(int i=0;i<n;i++)
                {
                    verstat111.add(verstat11.get(i));
                    verstat222.add(verstat22.get(i));
                    robot111.add(robot11.get(i));
                    robot222.add(robot22.get(i));
                    robot333.add(robot33.get(i));
                }
                for(int i = 0;i<n;i++)
                {
                    if(i==(n-1))
                    {
                    verstat111.add(verstat11.get(i));
                    verstat222.add(verstat22.get(i));
                    robot111.add(robot11.get(0));
                    robot222.add(robot22.get(0));
                    robot333.add(robot33.get(0));
                    }
                    else
                    {
                    verstat111.add(verstat11.get(i));
                    verstat222.add(verstat22.get(i));
                    robot111.add(robot11.get(i+1));
                    robot222.add(robot22.get(i+1));
                    robot333.add(robot33.get(i+1));
                    }
                }
                
                verstat1 = verstat111;
                verstat2 = verstat222;
                robot1 = robot111;
                robot2 = robot222;
                robot3 = robot333;
                
                n*=2;

                  System.out.println("Result: " + currentValue);
          }
     }
     
     public static double[] findBest(int timeModelling, int n, List<Integer> verstat1, List<Integer> verstat2, List<Integer> robot1, List<Integer> robot2,List<Integer> robot3 ) throws ExceptionInvalidNetStructure, ExceptionInvalidTimeDelay
     {
         int indexBest = 0;
         double valueBest = 0;
         double[] allValues = new double[n];
         
            for(int i = 0; i < n; i++)
            {
                ArrayList<PetriSim> list = new ArrayList<PetriSim>();
                list.add(new PetriSim(CreateRobots(verstat1.get(i), verstat2.get(i),robot1.get(i),robot2.get(i), robot3.get(i)))); 
                PetriObjModel model = new PetriObjModel(list);
                model.setIsProtokol(false);
                model.go(timeModelling);
                allValues[i] = model.getListObj().get(0).getNet().getListP()[14].getMark()/150000.0;
                if(model.getListObj().get(0).getNet().getListP()[14].getMark() > valueBest)
                {
                     indexBest = i;
                     valueBest = model.getListObj().get(0).getNet().getListP()[14].getMark();
                }
            }
            double[] result = new double[n+1];
            result[0] = indexBest;
            for(int i = 0; i<n;i++)
            {
                result[i+1] = allValues[i];
            }
            return result; 
     }
     
  
    
    public static PetriNet CreateRobots(int verstat1, int verstat2, int robot1, int robot2, int robot3) throws ExceptionInvalidTimeDelay {
	ArrayList<PetriP> d_P = new ArrayList<>();
	ArrayList<PetriT> d_T = new ArrayList<>();
	ArrayList<ArcIn> d_In = new ArrayList<>();
	ArrayList<ArcOut> d_Out = new ArrayList<>();
	d_P.add(new PetriP("P1",1));
	d_P.add(new PetriP("P2",0));
	d_P.add(new PetriP("P3",0));
	d_P.add(new PetriP("P4",0));
	d_P.add(new PetriP("P5",0));
	d_P.add(new PetriP("Вільні верстати 1",verstat1));
	d_P.add(new PetriP("P7",0));
	d_P.add(new PetriP("P8",0));
	d_P.add(new PetriP("P9",0));
	d_P.add(new PetriP("P10",0));
	d_P.add(new PetriP("Вільні верстати 2",verstat2));
	d_P.add(new PetriP("P12",0));
	d_P.add(new PetriP("P13",0));
	d_P.add(new PetriP("P14",0));
	d_P.add(new PetriP("Оброблені деталі",0));
	d_P.add(new PetriP("Вільні роботи 1",robot1));
	d_P.add(new PetriP("Вільні роботи 2",robot2));
	d_P.add(new PetriP("Вільні роботи 3",robot3));
	d_T.add(new PetriT("Надходження",10.0));
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
	d_In.add(new ArcIn(d_P.get(2),d_T.get(2),1));
	d_In.add(new ArcIn(d_P.get(0),d_T.get(0),1));
	d_In.add(new ArcIn(d_P.get(7),d_T.get(6),1));
	d_In.add(new ArcIn(d_P.get(1),d_T.get(1),1));
	d_In.add(new ArcIn(d_P.get(15),d_T.get(1),1));
	d_In.add(new ArcIn(d_P.get(3),d_T.get(3),1));
	d_In.add(new ArcIn(d_P.get(12),d_T.get(10),1));
	d_In.add(new ArcIn(d_P.get(11),d_T.get(9),1));
	d_In.add(new ArcIn(d_P.get(17),d_T.get(9),1));
	d_In.add(new ArcIn(d_P.get(6),d_T.get(5),1));
	d_In.add(new ArcIn(d_P.get(16),d_T.get(5),1));
	d_In.add(new ArcIn(d_P.get(9),d_T.get(8),1));
	d_In.add(new ArcIn(d_P.get(10),d_T.get(8),1));
	d_In.add(new ArcIn(d_P.get(4),d_T.get(4),1));
	d_In.add(new ArcIn(d_P.get(5),d_T.get(4),1));
	d_In.add(new ArcIn(d_P.get(8),d_T.get(7),1));
	d_In.add(new ArcIn(d_P.get(13),d_T.get(11),1));
	d_Out.add(new ArcOut(d_T.get(2),d_P.get(3),1));
	d_Out.add(new ArcOut(d_T.get(0),d_P.get(0),1));
	d_Out.add(new ArcOut(d_T.get(0),d_P.get(1),1));
	d_Out.add(new ArcOut(d_T.get(6),d_P.get(8),1));
	d_Out.add(new ArcOut(d_T.get(1),d_P.get(2),1));
	d_Out.add(new ArcOut(d_T.get(3),d_P.get(4),1));
	d_Out.add(new ArcOut(d_T.get(3),d_P.get(15),1));
	d_Out.add(new ArcOut(d_T.get(10),d_P.get(13),1));
	d_Out.add(new ArcOut(d_T.get(9),d_P.get(12),1));
	d_Out.add(new ArcOut(d_T.get(5),d_P.get(7),1));
	d_Out.add(new ArcOut(d_T.get(8),d_P.get(11),1));
	d_Out.add(new ArcOut(d_T.get(8),d_P.get(10),1));
	d_Out.add(new ArcOut(d_T.get(4),d_P.get(5),1));
	d_Out.add(new ArcOut(d_T.get(4),d_P.get(6),1));
	d_Out.add(new ArcOut(d_T.get(7),d_P.get(9),1));
	d_Out.add(new ArcOut(d_T.get(7),d_P.get(16),1));
	d_Out.add(new ArcOut(d_T.get(11),d_P.get(14),1));
	d_Out.add(new ArcOut(d_T.get(11),d_P.get(17),1));
	PetriNet d_Net = new PetriNet("graph",d_P,d_T,d_In,d_Out);
	PetriP.initNext();
	PetriT.initNext();
	ArcIn.initNext();
	ArcOut.initNext();

	return d_Net;
}
}
