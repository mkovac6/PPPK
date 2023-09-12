/*
 * To change this license header, choose License Headers in Project Properties.
 * To change this template file, choose Tools | Templates
 * and open the template in the editor.
 */
package hr.algebra.controller;

import hr.algebra.dao.RepositoryFactory;
import hr.algebra.viewmodel.PersonViewModel;
import java.net.URL;
import java.util.AbstractMap;
import java.util.Map;
import java.util.ResourceBundle;
import java.util.function.UnaryOperator;
import java.util.logging.Level;
import java.util.logging.Logger;
import java.util.stream.Collectors;
import java.util.stream.Stream;
import javafx.collections.FXCollections;
import javafx.collections.ObservableList;
import javafx.event.ActionEvent;
import javafx.fxml.FXML;
import javafx.fxml.Initializable;
import javafx.scene.control.Label;
import javafx.scene.control.Tab;
import javafx.scene.control.TabPane;
import javafx.scene.control.TableColumn;
import javafx.scene.control.TableView;
import javafx.scene.control.TextField;
import javafx.scene.control.TextFormatter;
import javafx.scene.image.ImageView;
import javafx.util.converter.IntegerStringConverter;

/**
 * FXML Controller class
 *
 * @author marko
 */
public class PeopleController implements Initializable {

    private Map<TextField, Label> validationMap;

    private final ObservableList<PersonViewModel> list = FXCollections.observableArrayList();

    private PersonViewModel selectedPersonViewModel;

    @FXML
    private TabPane tpContent;
    @FXML
    private Tab tabList;
    @FXML
    private TableColumn<PersonViewModel, String> tcFirstName, tcLastName, tcEmail;
    @FXML
    private TableColumn<PersonViewModel, Integer> tcAge;
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
    @FXML
    private TableView<PersonViewModel> tvPeople;

    /**
     * Initializes the controller class.
     */
    @Override
    public void initialize(URL url, ResourceBundle rb) {
        initValidation();
        initPeople();
        initTable();
        addIntegerMask(tfAge);
        setListeners();
    }

    @FXML
    private void edit() {

        if (tvPeople.getSelectionModel().getSelectedItem() != null) {
            bindPerson(tvPeople.getSelectionModel().getSelectedItem());
            tpContent.getSelectionModel().select(tabEdit);
        }

    }

    @FXML
    private void delete() {

        if (tvPeople.getSelectionModel().getSelectedItem() != null) {

        }

    }

    @FXML
    private void upload(ActionEvent event) {
    }

    @FXML
    private void commit(ActionEvent event) {
    }

    private void initValidation() {

        validationMap = Stream.of(
                new AbstractMap.SimpleImmutableEntry<>(tfFirstName, lbFirstName),
                new AbstractMap.SimpleImmutableEntry<>(tfLastName, lbLastName),
                new AbstractMap.SimpleImmutableEntry<>(tfAge, lbAge),
                new AbstractMap.SimpleImmutableEntry<>(tfEmail, lbEmail)
        ).collect(Collectors.toMap(Map.Entry::getKey, Map.Entry::getValue));

    }

    private void initPeople() {
        try {
            RepositoryFactory.getRepository().getPeople().forEach(person -> list.add(new PersonViewModel(person)));
        } catch (Exception ex) {
            Logger.getLogger(PeopleController.class.getName()).log(Level.SEVERE, null, ex);
        }
    }

    private void initTable() {
        tcFirstName.setCellValueFactory(person -> person.getValue().getFirstNameProperty());
        tcLastName.setCellValueFactory(person -> person.getValue().getLasttNameProperty());
        tcAge.setCellValueFactory(person -> person.getValue().getAgeProperty().asObject());
        tcEmail.setCellValueFactory(person -> person.getValue().getEmailProperty());
        tvPeople.setItems(list);
    }

    private void addIntegerMask(TextField tf) {

        UnaryOperator<TextFormatter.Change> filter = change -> {

            String input = change.getText();
            if (input.matches("\\d*")) {
                return change;
            }
            return null;
        };
        tf.setTextFormatter(new TextFormatter<>(new IntegerStringConverter(), 0, filter));

    }

    private void setListeners() {

        tpContent.setOnMouseClicked(enent -> {
            if (tpContent.getSelectionModel().getSelectedItem().equals(tabEdit)) {
                bindPerson(null);
            }
        });

    }

    private void bindPerson(PersonViewModel personViewModel) {
        selectedPersonViewModel = personViewModel != null ? personViewModel : new PersonViewModel(null);
        tfFirstName.setText(selectedPersonViewModel.getFirstNameProperty().get());
        tfLastName.setText(selectedPersonViewModel.getLasttNameProperty().get());
        tfAge.setText(String.valueOf(selectedPersonViewModel.getAgeProperty()));
        tfEmail.setText(selectedPersonViewModel.getEmailProperty().get());
    }
}
