/*
 * To change this license header, choose License Headers in Project Properties.
 * To change this template file, choose Tools | Templates
 * and open the template in the editor.
 */
package hr.algebra.dao.sql;

import javax.persistence.EntityManagerFactory;
import javax.persistence.Persistence;

/**
 *
 * @author marko
 */
public class HibernateFactory {
    
    private static final String PERSISTANCE_UNIT = "PeopleManagerPU";
    public static final String SELECT_ALL = "Person.findAll";
    private static final EntityManagerFactory EMF = Persistence.createEntityManagerFactory(PERSISTANCE_UNIT);
    
    public static EntityManagerWrapper getEntityManager(){
    
        return new EntityManagerWrapper(EMF.createEntityManager());
        
    }
    
    public static void release(){
    EMF.close();
    }
    
}
