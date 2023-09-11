/*
 * To change this license header, choose License Headers in Project Properties.
 * To change this template file, choose Tools | Templates
 * and open the template in the editor.
 */
package hr.algebra.dao.sql;

import hr.algebra.dao.Repository;
import hr.algebra.model.Person;
import java.util.List;
import javax.persistence.EntityManager;


public class HiberanteRepository implements Repository {

    @Override
    public int addPerson(Person data) throws Exception {
        try(hr.algebra.dao.Sql.EntityManagerWrapper wrapper = hr.algebra.dao.Sql.HibernateFactory.getEntityManager()){
            EntityManager em = wrapper.get();
            em.getTransaction().begin();
            Person person = new Person(data);
            em.persist(person);
            em.getTransaction().commit();
            return person.getIDPerson();
        }
    }

    @Override
    public void updatePerson(Person person) throws Exception {
        try(hr.algebra.dao.Sql.EntityManagerWrapper wrapper = hr.algebra.dao.Sql.HibernateFactory.getEntityManager()){
            EntityManager em = wrapper.get();
            em.getTransaction().begin();
            em.find(Person.class, person.getIDPerson()).updateDetails(person);
            em.getTransaction().commit();
        }
    }

    @Override
    public void deletePerson(Person person) throws Exception {
        try(hr.algebra.dao.Sql.EntityManagerWrapper wrapper = hr.algebra.dao.Sql.HibernateFactory.getEntityManager()){
            EntityManager em = wrapper.get();
            em.getTransaction().begin();
            em.remove(em.contains(person) ? person : em.merge(person));
            em.getTransaction().commit();
        }
    }

    @Override
    public Person getPerson(int idPerson) throws Exception {
        try(hr.algebra.dao.Sql.EntityManagerWrapper wrapper = hr.algebra.dao.Sql.HibernateFactory.getEntityManager()){
            EntityManager em = wrapper.get();
            return em.find(Person.class, idPerson);
        }
    }

    @Override
    public List<Person> getPeople() throws Exception {
        try(hr.algebra.dao.Sql.EntityManagerWrapper wrapper = hr.algebra.dao.Sql.HibernateFactory.getEntityManager()){
            EntityManager em = wrapper.get();
            return em.createNamedQuery(hr.algebra.dao.Sql.HibernateFactory.SELECT_ALL).getResultList();
        }
    }

    @Override
    public void release() {
       hr.algebra.dao.Sql.HibernateFactory.release();
    }
    
    
}
