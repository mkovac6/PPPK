/*
 * To change this license header, choose License Headers in Project Properties.
 * To change this template file, choose Tools | Templates
 * and open the template in the editor.
 */
package hr.algebra.controller;

import java.net.URL;
import java.util.ResourceBundle;
import javafx.event.ActionEvent;
import javafx.fxml.FXML;
import javafx.fxml.Initializable;
import javafx.scene.control.Label;
import javafx.scene.control.Tab;
import javafx.scene.control.TabPane;
import javafx.scene.control.TableColumn;
import javafx.scene.control.TableView;
import javafx.scene.control.TextField;
import javafx.scene.image.ImageView;

/**
 * FXML Controller class
 *
 * @author marko
 */
public class PeopleController implements Initializable {

    @FXML
    private TabPane tpContent;
    @FXML
    private Tab tabList;
    @FXML
    private TableView<?> tvPeople;
    @FXML
    private TableColumn<?, ?> tcFirstName;
    @FXML
    private TableColumn<?, ?> tcLastName;
    @FXML
    private TableColumn<?, ?> tcAge;
    @FXML
    private TableColumn<?, ?> tcEmail;
    @FXML
    private Tab tabEdit;
    @FXML
    private ImageView ivPerson;
    @FXML
    private TextField tfFirstName;
    @FXML
    private Label lbFirstName;
    @FXML
    private TextField tfLastName;
    @FXML
    private Label lbLastName;
    @FXML
    private TextField tfAge;
    @FXML
    private Label lbAge;
    @FXML
    private TextField tfEmail;
    @FXML
    private Label lbEmail;
    @FXML
    private Label lbIcon;

    /**
     * Initializes the controller class.
     */
    @Override
    public void initialize(URL url, ResourceBundle rb) {
        // TODO
    }    

    @FXML
    private void edit(ActionEvent event) {
    }

    @FXML
    private void delete(ActionEvent event) {
    }

    @FXML
    private void upload(ActionEvent event) {
    }

    @FXML
    private void commit(ActionEvent event) {
    }
    
}
