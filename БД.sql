CREATE DATABASE  IF NOT EXISTS `college` /*!40100 DEFAULT CHARACTER SET utf8mb4 COLLATE utf8mb4_0900_ai_ci */ /*!80016 DEFAULT ENCRYPTION='N' */;
USE `college`;
-- MySQL dump 10.13  Distrib 8.0.45, for Win64 (x86_64)
--
-- Host: localhost    Database: college
-- ------------------------------------------------------
-- Server version	8.0.45

/*!40101 SET @OLD_CHARACTER_SET_CLIENT=@@CHARACTER_SET_CLIENT */;
/*!40101 SET @OLD_CHARACTER_SET_RESULTS=@@CHARACTER_SET_RESULTS */;
/*!40101 SET @OLD_COLLATION_CONNECTION=@@COLLATION_CONNECTION */;
/*!50503 SET NAMES utf8 */;
/*!40103 SET @OLD_TIME_ZONE=@@TIME_ZONE */;
/*!40103 SET TIME_ZONE='+00:00' */;
/*!40014 SET @OLD_UNIQUE_CHECKS=@@UNIQUE_CHECKS, UNIQUE_CHECKS=0 */;
/*!40014 SET @OLD_FOREIGN_KEY_CHECKS=@@FOREIGN_KEY_CHECKS, FOREIGN_KEY_CHECKS=0 */;
/*!40101 SET @OLD_SQL_MODE=@@SQL_MODE, SQL_MODE='NO_AUTO_VALUE_ON_ZERO' */;
/*!40111 SET @OLD_SQL_NOTES=@@SQL_NOTES, SQL_NOTES=0 */;

--
-- Table structure for table `disciplines`
--

DROP TABLE IF EXISTS `disciplines`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!50503 SET character_set_client = utf8mb4 */;
CREATE TABLE `disciplines` (
  `discipline_id` varchar(20) NOT NULL,
  `discipline_name` varchar(255) NOT NULL,
  `speciality_code` varchar(20) NOT NULL,
  `hours` int DEFAULT NULL,
  `control_form` varchar(50) DEFAULT NULL,
  `semester` int DEFAULT NULL,
  `teacher_id` int DEFAULT NULL,
  PRIMARY KEY (`discipline_id`),
  KEY `fk_disciplines_speciality` (`speciality_code`),
  KEY `fk_disciplines_teacher` (`teacher_id`),
  CONSTRAINT `fk_disciplines_speciality` FOREIGN KEY (`speciality_code`) REFERENCES `specialities` (`speciality_code`) ON DELETE RESTRICT ON UPDATE CASCADE,
  CONSTRAINT `fk_disciplines_teacher` FOREIGN KEY (`teacher_id`) REFERENCES `teachers` (`teacher_id`) ON DELETE SET NULL ON UPDATE CASCADE
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_0900_ai_ci;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `disciplines`
--

LOCK TABLES `disciplines` WRITE;
/*!40000 ALTER TABLE `disciplines` DISABLE KEYS */;
INSERT INTO `disciplines` VALUES ('ДИС-001','Основы программирования','09.02.07',144,'экзамен',1,1),('ДИС-002','Базы данных','09.02.07',72,'зачёт',2,1),('ДИС-003','Алгоритмы и структуры данных','09.02.07',54,'зачёт',1,1),('ДИС-004','Web-разработка','09.02.07',90,'экзамен',3,2),('ДИС-005','Проектирование ИС','09.02.07',54,'зачёт',3,2),('ДИС-006','Информационная безопасность','09.02.07',36,'зачёт',4,2),('ДИС-007','Сети и телекоммуникации','09.02.07',72,'экзамен',4,2),('ДИС-008','Операционные системы','09.02.07',54,'зачёт',2,1),('ДИС-009','Бухгалтерский учёт','38.02.01',180,'экзамен',1,3),('ДИС-010','Налогообложение','38.02.01',90,'экзамен',2,3),('ДИС-011','Финансовый анализ','38.02.01',72,'зачёт',3,3),('ДИС-012','Аудит','38.02.01',54,'зачёт',4,3),('ДИС-013','Управленческий учёт','38.02.01',72,'экзамен',3,3),('ДИС-014','Технология обработки древесины','35.02.03',200,'экзамен',1,4),('ДИС-015','Материаловедение','35.02.03',90,'зачёт',2,4),('ДИС-016','Деревообрабатывающие станки','35.02.03',120,'экзамен',2,4),('ДИС-017','Охрана труда','35.02.03',36,'зачёт',3,4),('ДИС-018','Конструирование изделий','35.02.03',72,'зачёт',3,4),('ДИС-019','Станочное оборудование','15.02.16',180,'экзамен',1,5),('ДИС-020','Технологические процессы','15.02.16',120,'экзамен',2,5),('ДИС-021','Техническое черчение','15.02.16',90,'зачёт',1,5),('ДИС-022','Метрология','15.02.16',54,'зачёт',2,5),('ДИС-023','Программирование станков с ЧПУ','15.02.16',72,'экзамен',3,5),('ДИС-024','Гражданское право','40.02.01',144,'экзамен',1,6),('ДИС-025','Трудовое право','40.02.01',90,'экзамен',2,6),('ДИС-026','Право социального обеспечения','40.02.01',90,'зачёт',3,6),('ДИС-027','Семейное право','40.02.01',54,'зачёт',4,6),('ДИС-028','Уголовное право','40.02.04',180,'экзамен',1,6),('ДИС-029','Конституционное право','40.02.04',90,'экзамен',2,6),('ДИС-030','Административное право','40.02.04',72,'зачёт',2,6),('ДИС-031','Арбитражный процесс','40.02.04',54,'зачёт',3,6),('ДИС-032','Математика','09.02.07',108,'экзамен',1,7),('ДИС-033','Математика','38.02.01',72,'зачёт',1,7),('ДИС-034','Физика','15.02.16',72,'зачёт',1,7),('ДИС-035','Экономика организации','38.02.01',90,'экзамен',2,3);
/*!40000 ALTER TABLE `disciplines` ENABLE KEYS */;
UNLOCK TABLES;

--
-- Table structure for table `licenses`
--

DROP TABLE IF EXISTS `licenses`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!50503 SET character_set_client = utf8mb4 */;
CREATE TABLE `licenses` (
  `license_id` int NOT NULL AUTO_INCREMENT,
  `series_number` varchar(50) NOT NULL,
  `issue_date` date DEFAULT NULL,
  `issuer` varchar(255) DEFAULT NULL,
  PRIMARY KEY (`license_id`)
) ENGINE=InnoDB AUTO_INCREMENT=2 DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_0900_ai_ci;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `licenses`
--

LOCK TABLES `licenses` WRITE;
/*!40000 ALTER TABLE `licenses` DISABLE KEYS */;
INSERT INTO `licenses` VALUES (1,'90Л01 №0009591','2019-03-01','Рособрнадзор');
/*!40000 ALTER TABLE `licenses` ENABLE KEYS */;
UNLOCK TABLES;

--
-- Table structure for table `performance`
--

DROP TABLE IF EXISTS `performance`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!50503 SET character_set_client = utf8mb4 */;
CREATE TABLE `performance` (
  `performance_id` int NOT NULL AUTO_INCREMENT,
  `record_book_no` varchar(20) NOT NULL,
  `discipline_id` varchar(20) NOT NULL,
  `semester` int DEFAULT NULL,
  `grade` decimal(3,1) DEFAULT NULL,
  `attestation_date` date DEFAULT NULL,
  `teacher_id` int DEFAULT NULL,
  PRIMARY KEY (`performance_id`),
  KEY `fk_performance_student` (`record_book_no`),
  KEY `fk_performance_discipline` (`discipline_id`),
  KEY `fk_performance_teacher` (`teacher_id`),
  CONSTRAINT `fk_performance_discipline` FOREIGN KEY (`discipline_id`) REFERENCES `disciplines` (`discipline_id`) ON DELETE RESTRICT ON UPDATE CASCADE,
  CONSTRAINT `fk_performance_student` FOREIGN KEY (`record_book_no`) REFERENCES `students` (`record_book_no`) ON DELETE CASCADE ON UPDATE CASCADE,
  CONSTRAINT `fk_performance_teacher` FOREIGN KEY (`teacher_id`) REFERENCES `teachers` (`teacher_id`) ON DELETE SET NULL ON UPDATE CASCADE
) ENGINE=InnoDB AUTO_INCREMENT=37 DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_0900_ai_ci;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `performance`
--

LOCK TABLES `performance` WRITE;
/*!40000 ALTER TABLE `performance` DISABLE KEYS */;
INSERT INTO `performance` VALUES (1,'2024-ИСП-001','ДИС-001',1,5.0,'2025-01-15',1),(2,'2024-ИСП-001','ДИС-002',2,4.0,'2025-06-15',1),(3,'2024-ИСП-001','ДИС-032',1,4.0,'2025-01-15',7),(4,'2024-ИСП-002','ДИС-001',1,4.0,'2025-01-15',1),(5,'2024-ИСП-002','ДИС-002',2,5.0,'2025-06-15',1),(6,'2024-ИСП-002','ДИС-032',1,5.0,'2025-01-15',7),(7,'2024-ИСП-004','ДИС-001',1,3.0,'2025-01-15',1),(8,'2024-ИСП-005','ДИС-001',1,5.0,'2025-01-15',1),(9,'2023-ИСП-021','ДИС-004',3,5.0,'2026-01-20',2),(10,'2023-ИСП-021','ДИС-005',3,4.0,'2026-01-20',2),(11,'2023-ИСП-023','ДИС-004',3,4.0,'2026-01-20',2),(12,'2023-ИСП-024','ДИС-004',3,2.0,'2026-01-20',2),(13,'2024-ЭБ-001','ДИС-009',1,5.0,'2025-01-15',3),(14,'2024-ЭБ-001','ДИС-010',2,4.0,'2025-06-15',3),(15,'2024-ЭБ-001','ДИС-033',1,5.0,'2025-01-15',7),(16,'2024-ЭБ-002','ДИС-009',1,3.0,'2025-01-15',3),(17,'2024-ЭБ-003','ДИС-009',1,4.0,'2025-01-15',3),(18,'2023-ЭБ-011','ДИС-010',2,5.0,'2025-06-15',3),(19,'2023-ЭБ-011','ДИС-011',3,4.0,'2026-01-20',3),(20,'2023-ЭБ-012','ДИС-010',2,4.0,'2025-06-15',3),(21,'2024-ТД-001','ДИС-014',1,4.0,'2025-01-15',4),(22,'2024-ТД-002','ДИС-014',1,5.0,'2025-01-15',4),(23,'2024-ТД-003','ДИС-014',1,3.0,'2025-01-15',4),(24,'2024-ТД-005','ДИС-014',1,5.0,'2025-01-15',4),(25,'2024-ТМ-001','ДИС-019',1,5.0,'2025-01-15',5),(26,'2024-ТМ-002','ДИС-019',1,2.0,'2025-01-15',5),(27,'2024-ТМ-003','ДИС-019',1,4.0,'2025-01-15',5),(28,'2024-ТМ-004','ДИС-019',1,5.0,'2025-01-15',5),(29,'2024-ПО-001','ДИС-024',1,4.0,'2025-01-15',6),(30,'2024-ПО-002','ДИС-024',1,5.0,'2025-01-15',6),(31,'2024-ПО-003','ДИС-024',1,3.0,'2025-01-15',6),(32,'2024-ЮР-001','ДИС-028',1,5.0,'2025-01-15',6),(33,'2024-ЮР-002','ДИС-028',1,4.0,'2025-01-15',6),(34,'2023-ЮР-011','ДИС-029',2,5.0,'2025-06-15',6),(35,'2023-ЮР-012','ДИС-029',2,4.0,'2025-06-15',6);
/*!40000 ALTER TABLE `performance` ENABLE KEYS */;
UNLOCK TABLES;
/*!50003 SET @saved_cs_client      = @@character_set_client */ ;
/*!50003 SET @saved_cs_results     = @@character_set_results */ ;
/*!50003 SET @saved_col_connection = @@collation_connection */ ;
/*!50003 SET character_set_client  = utf8mb4 */ ;
/*!50003 SET character_set_results = utf8mb4 */ ;
/*!50003 SET collation_connection  = utf8mb4_0900_ai_ci */ ;
/*!50003 SET @saved_sql_mode       = @@sql_mode */ ;
/*!50003 SET sql_mode              = 'ONLY_FULL_GROUP_BY,STRICT_TRANS_TABLES,NO_ZERO_IN_DATE,NO_ZERO_DATE,ERROR_FOR_DIVISION_BY_ZERO,NO_ENGINE_SUBSTITUTION' */ ;
DELIMITER ;;
/*!50003 CREATE*/ /*!50017 DEFINER=`root`@`localhost`*/ /*!50003 TRIGGER `trg_performance_before_insert` BEFORE INSERT ON `performance` FOR EACH ROW BEGIN
    IF NEW.grade IS NULL OR NEW.grade NOT IN (2.0, 3.0, 4.0, 5.0) THEN
        SIGNAL SQLSTATE '45000'
        SET MESSAGE_TEXT = 'Оценка должна быть одной из значений: 2, 3, 4, 5';
    END IF;
END */;;
DELIMITER ;
/*!50003 SET sql_mode              = @saved_sql_mode */ ;
/*!50003 SET character_set_client  = @saved_cs_client */ ;
/*!50003 SET character_set_results = @saved_cs_results */ ;
/*!50003 SET collation_connection  = @saved_col_connection */ ;

--
-- Table structure for table `schedule`
--

DROP TABLE IF EXISTS `schedule`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!50503 SET character_set_client = utf8mb4 */;
CREATE TABLE `schedule` (
  `schedule_id` int NOT NULL AUTO_INCREMENT,
  `academic_year` varchar(20) NOT NULL,
  `semester` int DEFAULT NULL,
  `day_of_week` varchar(20) DEFAULT NULL,
  `pair_no` int DEFAULT NULL,
  `start_time` time DEFAULT NULL,
  `end_time` time DEFAULT NULL,
  `group_id` int NOT NULL,
  `discipline_id` varchar(20) NOT NULL,
  `teacher_id` int DEFAULT NULL,
  `cabinet` varchar(20) DEFAULT NULL,
  `lesson_type` varchar(50) DEFAULT NULL,
  PRIMARY KEY (`schedule_id`),
  KEY `fk_schedule_group` (`group_id`),
  KEY `fk_schedule_discipline` (`discipline_id`),
  KEY `fk_schedule_teacher` (`teacher_id`),
  CONSTRAINT `fk_schedule_discipline` FOREIGN KEY (`discipline_id`) REFERENCES `disciplines` (`discipline_id`) ON DELETE RESTRICT ON UPDATE CASCADE,
  CONSTRAINT `fk_schedule_group` FOREIGN KEY (`group_id`) REFERENCES `student_groups` (`group_id`) ON DELETE CASCADE ON UPDATE CASCADE,
  CONSTRAINT `fk_schedule_teacher` FOREIGN KEY (`teacher_id`) REFERENCES `teachers` (`teacher_id`) ON DELETE SET NULL ON UPDATE CASCADE
) ENGINE=InnoDB AUTO_INCREMENT=41 DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_0900_ai_ci;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `schedule`
--

LOCK TABLES `schedule` WRITE;
/*!40000 ALTER TABLE `schedule` DISABLE KEYS */;
INSERT INTO `schedule` VALUES (1,'2024/2025',1,'Пятница',1,'08:00:00','09:35:00',1,'ДИС-001',1,'305','лекция'),(2,'2024/2025',1,'Понедельник',2,'09:45:00','11:20:00',1,'ДИС-001',1,'305','практика'),(3,'2024/2025',1,'Понедельник',3,'11:30:00','13:05:00',1,'ДИС-033',7,'204','лекция'),(4,'2024/2025',1,'Понедельник',1,'08:00:00','09:35:00',4,'ДИС-009',3,'201','лекция'),(5,'2024/2025',1,'Понедельник',2,'09:45:00','11:20:00',4,'ДИС-009',3,'201','практика'),(6,'2024/2025',1,'Вторник',1,'08:00:00','09:35:00',7,'ДИС-014',4,'102','лекция'),(7,'2024/2025',1,'Вторник',2,'09:45:00','11:20:00',7,'ДИС-014',4,'102','практика'),(8,'2024/2025',1,'Вторник',1,'08:00:00','09:35:00',10,'ДИС-019',5,'115','лекция'),(9,'2024/2025',1,'Вторник',2,'09:45:00','11:20:00',10,'ДИС-019',5,'115','практика'),(10,'2024/2025',1,'Вторник',3,'11:30:00','13:05:00',10,'ДИС-021',5,'115','практика'),(11,'2024/2025',1,'Среда',1,'08:00:00','09:35:00',13,'ДИС-024',6,'210','лекция'),(12,'2024/2025',1,'Среда',2,'09:45:00','11:20:00',13,'ДИС-024',6,'210','практика'),(13,'2024/2025',1,'Среда',1,'08:00:00','09:35:00',16,'ДИС-028',6,'210','лекция'),(14,'2024/2025',1,'Среда',2,'09:45:00','11:20:00',16,'ДИС-028',6,'210','практика'),(15,'2024/2025',1,'Четверг',1,'08:00:00','09:35:00',1,'ДИС-003',1,'305','лекция'),(16,'2024/2025',1,'Четверг',2,'09:45:00','11:20:00',1,'ДИС-003',1,'305','практика'),(17,'2024/2025',1,'Четверг',1,'08:00:00','09:35:00',4,'ДИС-033',7,'204','лекция'),(18,'2024/2025',1,'Четверг',1,'08:00:00','09:35:00',7,'ДИС-015',4,'102','лекция'),(19,'2024/2025',1,'Пятница',1,'08:00:00','09:35:00',2,'ДИС-004',2,'307','лекция'),(20,'2024/2025',1,'Пятница',2,'09:45:00','11:20:00',2,'ДИС-004',2,'307','практика'),(21,'2024/2025',2,'Понедельник',1,'08:00:00','09:35:00',1,'ДИС-002',1,'305','лекция'),(22,'2024/2025',2,'Понедельник',2,'09:45:00','11:20:00',1,'ДИС-002',1,'305','практика'),(23,'2024/2025',2,'Понедельник',1,'08:00:00','09:35:00',4,'ДИС-010',3,'201','лекция'),(24,'2024/2025',2,'Вторник',1,'08:00:00','09:35:00',7,'ДИС-016',4,'102','лекция'),(25,'2024/2025',2,'Вторник',2,'09:45:00','11:20:00',7,'ДИС-016',4,'102','практика'),(26,'2024/2025',2,'Вторник',1,'08:00:00','09:35:00',10,'ДИС-020',5,'115','лекция'),(27,'2024/2025',2,'Среда',1,'08:00:00','09:35:00',16,'ДИС-029',6,'210','лекция'),(28,'2024/2025',2,'Среда',1,'08:00:00','09:35:00',13,'ДИС-025',6,'210','лекция'),(29,'2024/2025',3,'Понедельник',1,'08:00:00','09:35:00',2,'ДИС-005',2,'307','лекция'),(30,'2024/2025',3,'Понедельник',2,'09:45:00','11:20:00',2,'ДИС-005',2,'307','практика'),(31,'2024/2025',3,'Вторник',1,'08:00:00','09:35:00',5,'ДИС-011',3,'201','лекция'),(32,'2024/2025',3,'Среда',1,'08:00:00','09:35:00',8,'ДИС-018',4,'102','лекция'),(33,'2024/2025',3,'Четверг',1,'08:00:00','09:35:00',11,'ДИС-023',5,'115','лекция'),(34,'2024/2025',4,'Понедельник',1,'08:00:00','09:35:00',2,'ДИС-006',2,'307','лекция'),(35,'2024/2025',4,'Вторник',1,'08:00:00','09:35:00',5,'ДИС-012',3,'201','лекция');
/*!40000 ALTER TABLE `schedule` ENABLE KEYS */;
UNLOCK TABLES;

--
-- Table structure for table `specialities`
--

DROP TABLE IF EXISTS `specialities`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!50503 SET character_set_client = utf8mb4 */;
CREATE TABLE `specialities` (
  `speciality_code` varchar(20) NOT NULL,
  `speciality_name` varchar(255) NOT NULL,
  `qualification` varchar(255) DEFAULT NULL,
  `study_duration` varchar(50) DEFAULT NULL,
  `study_form` varchar(50) DEFAULT NULL,
  `admission_year` year DEFAULT NULL,
  `budget_places` int DEFAULT NULL,
  `paid_places` int DEFAULT NULL,
  `license_id` int DEFAULT NULL,
  PRIMARY KEY (`speciality_code`),
  KEY `fk_specialities_license` (`license_id`),
  CONSTRAINT `fk_specialities_license` FOREIGN KEY (`license_id`) REFERENCES `licenses` (`license_id`) ON DELETE SET NULL ON UPDATE CASCADE
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_0900_ai_ci;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `specialities`
--

LOCK TABLES `specialities` WRITE;
/*!40000 ALTER TABLE `specialities` DISABLE KEYS */;
INSERT INTO `specialities` VALUES ('09.02.07','Информационные системы и программирование','Программист','3 г. 10 мес.','очная',2023,25,10,1),('15.02.16','Технология машиностроения','Техник','3 г. 10 мес.','очная',2023,20,5,1),('35.02.03','Технология деревообработки','Техник','3 г. 10 мес.','очная',2022,20,5,1),('38.02.01','Экономика и бухгалтерский учёт (по отраслям)','Бухгалтер','2 г. 10 мес.','очная',2022,25,5,1),('40.02.01','Право и организация социального обеспечения','Юрист','2 г. 10 мес.','очная',2024,20,5,1),('40.02.04','Юриспруденция','Юрист','2 г. 10 мес.','очная',2024,20,5,1);
/*!40000 ALTER TABLE `specialities` ENABLE KEYS */;
UNLOCK TABLES;

--
-- Table structure for table `student_groups`
--

DROP TABLE IF EXISTS `student_groups`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!50503 SET character_set_client = utf8mb4 */;
CREATE TABLE `student_groups` (
  `group_id` int NOT NULL AUTO_INCREMENT,
  `group_number` varchar(50) NOT NULL,
  `speciality_code` varchar(20) NOT NULL,
  `admission_year` year DEFAULT NULL,
  `graduation_year` year DEFAULT NULL,
  `study_form` varchar(50) DEFAULT NULL,
  `teacher_id` int DEFAULT NULL,
  `student_count` int DEFAULT NULL,
  `meeting_room` varchar(20) DEFAULT NULL,
  `building` varchar(100) DEFAULT NULL,
  PRIMARY KEY (`group_id`),
  KEY `fk_groups_speciality` (`speciality_code`),
  KEY `fk_groups_teacher` (`teacher_id`),
  CONSTRAINT `fk_groups_speciality` FOREIGN KEY (`speciality_code`) REFERENCES `specialities` (`speciality_code`) ON DELETE RESTRICT ON UPDATE CASCADE,
  CONSTRAINT `fk_groups_teacher` FOREIGN KEY (`teacher_id`) REFERENCES `teachers` (`teacher_id`) ON DELETE SET NULL ON UPDATE CASCADE
) ENGINE=InnoDB AUTO_INCREMENT=25 DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_0900_ai_ci;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `student_groups`
--

LOCK TABLES `student_groups` WRITE;
/*!40000 ALTER TABLE `student_groups` DISABLE KEYS */;
INSERT INTO `student_groups` VALUES (1,'ИСП-41','09.02.07',2024,2028,'очная',1,5,'305','Главный'),(2,'ИСП-31','09.02.07',2023,2027,'очная',2,5,'307','Главный'),(3,'ИСП-21','09.02.07',2022,2026,'очная',2,4,'307','Главный'),(4,'ЭБ-41','38.02.01',2024,2027,'очная',3,4,'201','Главный'),(5,'ЭБ-31','38.02.01',2023,2026,'очная',3,3,'201','Главный'),(6,'ЭБ-21','38.02.01',2022,2025,'очная',9,3,'302','Главный'),(7,'ТД-41','35.02.03',2024,2028,'очная',4,5,'102','Мастерские'),(8,'ТД-31','35.02.03',2023,2027,'очная',4,4,'102','Мастерские'),(9,'ТД-21','35.02.03',2022,2026,'очная',10,4,'103','Мастерские'),(10,'ТМ-41','15.02.16',2024,2028,'очная',5,4,'115','Мастерские'),(11,'ТМ-31','15.02.16',2023,2027,'очная',5,3,'115','Мастерские'),(12,'ТМ-21','15.02.16',2022,2026,'очная',10,3,'103','Мастерские'),(13,'ПО-41','40.02.01',2024,2027,'очная',6,3,'210','Главный'),(14,'ПО-31','40.02.01',2023,2026,'очная',6,3,'210','Главный'),(15,'ПО-21','40.02.01',2022,2025,'очная',9,4,'302','Главный'),(16,'ЮР-41','40.02.04',2024,2027,'очная',6,2,'210','Главный'),(17,'ЮР-31','40.02.04',2023,2026,'очная',6,4,'210','Главный'),(18,'ЮР-21','40.02.04',2022,2025,'очная',9,4,'302','Главный'),(19,'ИСП-11','09.02.07',2021,2025,'очная',1,6,'305','Главный'),(20,'ЭБ-11','38.02.01',2021,2024,'очная',3,5,'201','Главный'),(21,'ТД-11','35.02.03',2021,2025,'очная',4,5,'102','Мастерские'),(22,'ТМ-11','15.02.16',2021,2025,'очная',5,4,'115','Мастерские'),(23,'ПО-11','40.02.01',2021,2024,'очная',6,5,'210','Главный'),(24,'ЮР-11','40.02.04',2021,2024,'очная',6,5,'210','Главный');
/*!40000 ALTER TABLE `student_groups` ENABLE KEYS */;
UNLOCK TABLES;
/*!50003 SET @saved_cs_client      = @@character_set_client */ ;
/*!50003 SET @saved_cs_results     = @@character_set_results */ ;
/*!50003 SET @saved_col_connection = @@collation_connection */ ;
/*!50003 SET character_set_client  = utf8mb4 */ ;
/*!50003 SET character_set_results = utf8mb4 */ ;
/*!50003 SET collation_connection  = utf8mb4_0900_ai_ci */ ;
/*!50003 SET @saved_sql_mode       = @@sql_mode */ ;
/*!50003 SET sql_mode              = 'ONLY_FULL_GROUP_BY,STRICT_TRANS_TABLES,NO_ZERO_IN_DATE,NO_ZERO_DATE,ERROR_FOR_DIVISION_BY_ZERO,NO_ENGINE_SUBSTITUTION' */ ;
DELIMITER ;;
/*!50003 CREATE*/ /*!50017 DEFINER=`root`@`localhost`*/ /*!50003 TRIGGER `trg_groups_before_delete` BEFORE DELETE ON `student_groups` FOR EACH ROW BEGIN
    IF EXISTS (
        SELECT 1
        FROM students s
        WHERE s.group_id = OLD.group_id
        LIMIT 1
    ) THEN
        SIGNAL SQLSTATE '45000'
        SET MESSAGE_TEXT = 'Нельзя удалить группу: в ней есть студенты';
    END IF;
END */;;
DELIMITER ;
/*!50003 SET sql_mode              = @saved_sql_mode */ ;
/*!50003 SET character_set_client  = @saved_cs_client */ ;
/*!50003 SET character_set_results = @saved_cs_results */ ;
/*!50003 SET collation_connection  = @saved_col_connection */ ;

--
-- Table structure for table `students`
--

DROP TABLE IF EXISTS `students`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!50503 SET character_set_client = utf8mb4 */;
CREATE TABLE `students` (
  `record_book_no` varchar(20) NOT NULL,
  `surname` varchar(100) NOT NULL,
  `name` varchar(100) NOT NULL,
  `patronymic` varchar(100) DEFAULT NULL,
  `birth_date` date DEFAULT NULL,
  `group_id` int NOT NULL,
  `student_status` varchar(50) DEFAULT NULL,
  `study_form` varchar(50) DEFAULT NULL,
  `photo_path` varchar(255) DEFAULT NULL,
  PRIMARY KEY (`record_book_no`),
  KEY `fk_students_group` (`group_id`),
  CONSTRAINT `fk_students_group` FOREIGN KEY (`group_id`) REFERENCES `student_groups` (`group_id`) ON DELETE RESTRICT ON UPDATE CASCADE
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_0900_ai_ci;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `students`
--

LOCK TABLES `students` WRITE;
/*!40000 ALTER TABLE `students` DISABLE KEYS */;
INSERT INTO `students` VALUES ('2023-ИСП-021','Новикова','Виктория','Павловна','2005-05-10',2,'обучается','очная','C:\\Users\\User\\source\\repos\\WindowsFormsApp2\\Resources\\picture(2023-ИСП-021).png'),('2023-ИСП-022','Фёдоров','Игорь','Николаевич','2005-01-03',2,'отчислен','очная','C:\\Users\\User\\source\\repos\\WindowsFormsApp2\\Resources\\picture.png'),('2023-ИСП-023','Захарова','Полина','Андреевна','2005-08-22',2,'обучается','очная','C:\\Users\\User\\source\\repos\\WindowsFormsApp2\\Resources\\picture.png'),('2023-ИСП-024','Тарасов','Владислав','Олегович','2004-12-14',2,'обучается','очная','C:\\Users\\User\\source\\repos\\WindowsFormsApp2\\Resources\\picture(2023-ИСП-024).png'),('2023-ИСП-025','Крылова','Диана','Романовна','2005-03-05',2,'обучается','очная','C:\\Users\\User\\source\\repos\\WindowsFormsApp2\\Resources\\picture.png'),('2023-ЭБ-011','Морозова','Анастасия','Владимировна','2005-12-06',5,'обучается','очная','C:\\Users\\User\\source\\repos\\WindowsFormsApp2\\Resources\\picture.png'),('2023-ЭБ-012','Волкова','Ирина','Дмитриевна','2005-07-24',5,'обучается','очная','C:\\Users\\User\\source\\repos\\WindowsFormsApp2\\Resources\\picture(2023-ЭБ-012).png'),('2023-ЭБ-013','Семёнов','Николай','Артёмович','2005-02-09',5,'отчислен','очная','C:\\Users\\User\\source\\repos\\WindowsFormsApp2\\Resources\\picture.png'),('2023-ЮР-011','Васильев','Роман','Кириллович','2005-07-19',17,'обучается','очная','C:\\Users\\User\\source\\repos\\WindowsFormsApp2\\Resources\\picture.png'),('2023-ЮР-012','Панова','Татьяна','Владимировна','2005-11-08',17,'обучается','очная','C:\\Users\\User\\source\\repos\\WindowsFormsApp2\\Resources\\picture.png'),('2023-ЮР-013','Носков','Иван','Сергеевич','2005-04-25',17,'академический отпуск','очная','C:\\Users\\User\\source\\repos\\WindowsFormsApp2\\Resources\\picture(2023-ЮР-013).png'),('2023-ЮР-014','Рогова','Кристина','Павловна','2005-06-13',17,'обучается','очная','C:\\Users\\User\\source\\repos\\WindowsFormsApp2\\Resources\\picture.png'),('2024-ИСП-001','Иванов','Алексей','Сергеевич','2006-03-15',1,'обучается','очная','C:\\Users\\User\\source\\repos\\WindowsFormsApp2\\Resources\\picture(2024-ИСП-001).png'),('2024-ИСП-002','Смирнова','Ольга','Дмитриевна','2006-07-02',1,'обучается','очная','C:\\Users\\User\\source\\repos\\WindowsFormsApp2\\Resources\\picture.png'),('2024-ИСП-003','Козлов','Дмитрий','Андреевич','2005-11-29',1,'академический отпуск','очная','C:\\Users\\User\\source\\repos\\WindowsFormsApp2\\Resources\\picture.png'),('2024-ИСП-004','Архипова','Валерия','Игоревна','2006-04-07',1,'обучается','очная','C:\\Users\\User\\source\\repos\\WindowsFormsApp2\\Resources\\picture(2024-ИСП-004).png'),('2024-ИСП-005','Никитин','Роман','Павлович','2006-09-18',1,'обучается','очная','C:\\Users\\User\\source\\repos\\WindowsFormsApp2\\Resources\\picture.png'),('2024-ПО-001','Тихонов','Максим','Денисович','2006-10-11',13,'обучается','очная','C:\\Users\\User\\source\\repos\\WindowsFormsApp2\\Resources\\picture.png'),('2024-ПО-002','Коновалова','Светлана','Юрьевна','2006-07-28',13,'обучается','очная','C:\\Users\\User\\source\\repos\\WindowsFormsApp2\\Resources\\picture.png'),('2024-ПО-003','Малышев','Антон','Витальевич','2006-03-15',13,'обучается','очная','C:\\Users\\User\\source\\repos\\WindowsFormsApp2\\Resources\\picture(2024-ПО-003).png'),('2024-ТД-001','Орлов','Павел','Степанович','2006-02-14',7,'обучается','очная','C:\\Users\\User\\source\\repos\\WindowsFormsApp2\\Resources\\picture.png'),('2024-ТД-002','Зайцева','Дарья','Романовна','2006-08-08',7,'обучается','очная','C:\\Users\\User\\source\\repos\\WindowsFormsApp2\\Resources\\picture(2024-ТД-002).png'),('2024-ТД-003','Пономарёв','Илья','Васильевич','2006-05-27',7,'обучается','очная','C:\\Users\\User\\source\\repos\\WindowsFormsApp2\\Resources\\picture.png'),('2024-ТД-004','Гусева','Алина','Николаевна','2006-11-03',7,'академический отпуск','очная','C:\\Users\\User\\source\\repos\\WindowsFormsApp2\\Resources\\picture.png'),('2024-ТД-005','Кириллов','Степан','Денисович','2006-04-16',7,'обучается','очная','C:\\Users\\User\\source\\repos\\WindowsFormsApp2\\Resources\\picture(2024-ТД-005).png'),('2024-ТМ-001','Соколов','Артём','Викторович','2006-06-23',10,'обучается','очная','C:\\Users\\User\\source\\repos\\WindowsFormsApp2\\Resources\\picture.png'),('2024-ТМ-002','Белова','Екатерина','Олеговна','2006-03-30',10,'обучается','очная','C:\\Users\\User\\source\\repos\\WindowsFormsApp2\\Resources\\picture.png'),('2024-ТМ-003','Дементьев','Сергей','Алексеевич','2006-10-11',10,'обучается','очная','C:\\Users\\User\\source\\repos\\WindowsFormsApp2\\Resources\\picture.png'),('2024-ТМ-004','Щербакова','Елена','Кирилловна','2006-01-02',10,'обучается','очная','C:\\Users\\User\\source\\repos\\WindowsFormsApp2\\Resources\\picture(2024-ТМ-004).png'),('2024-ЭБ-001','Попова','Мария','Игоревна','2006-04-21',4,'обучается','очная','C:\\Users\\User\\source\\repos\\WindowsFormsApp2\\Resources\\picture.png'),('2024-ЭБ-002','Лебедев','Кирилл','Алексеевич','2006-09-17',4,'обучается','очная','C:\\Users\\User\\source\\repos\\WindowsFormsApp2\\Resources\\picture(2024-ЭБ-002).png'),('2024-ЭБ-003','Соловьёва','Анна','Петровна','2006-01-30',4,'обучается','очная','C:\\Users\\User\\source\\repos\\WindowsFormsApp2\\Resources\\picture.png'),('2024-ЭБ-004','Большаков','Евгений','Сергеевич','2006-06-12',4,'обучается','очная','C:\\Users\\User\\source\\repos\\WindowsFormsApp2\\Resources\\picture(2024-ЭБ-004).png'),('2024-ЮР-001','Громова','Юлия','Сергеевна','2006-05-05',16,'обучается','очная','C:\\Users\\User\\source\\repos\\WindowsFormsApp2\\Resources\\picture.png'),('2024-ЮР-002','Беляев','Артур','Олегович','2006-09-19',16,'обучается','очная','C:\\Users\\User\\source\\repos\\WindowsFormsApp2\\Resources\\picture(2024-ЮР-002).png');
/*!40000 ALTER TABLE `students` ENABLE KEYS */;
UNLOCK TABLES;
/*!50003 SET @saved_cs_client      = @@character_set_client */ ;
/*!50003 SET @saved_cs_results     = @@character_set_results */ ;
/*!50003 SET @saved_col_connection = @@collation_connection */ ;
/*!50003 SET character_set_client  = utf8mb4 */ ;
/*!50003 SET character_set_results = utf8mb4 */ ;
/*!50003 SET collation_connection  = utf8mb4_0900_ai_ci */ ;
/*!50003 SET @saved_sql_mode       = @@sql_mode */ ;
/*!50003 SET sql_mode              = 'ONLY_FULL_GROUP_BY,STRICT_TRANS_TABLES,NO_ZERO_IN_DATE,NO_ZERO_DATE,ERROR_FOR_DIVISION_BY_ZERO,NO_ENGINE_SUBSTITUTION' */ ;
DELIMITER ;;
/*!50003 CREATE*/ /*!50017 DEFINER=`root`@`localhost`*/ /*!50003 TRIGGER `trg_students_before_delete` BEFORE DELETE ON `students` FOR EACH ROW BEGIN
    IF EXISTS (
        SELECT 1
        FROM performance p
        WHERE p.record_book_no = OLD.record_book_no
        LIMIT 1
    ) THEN
        SIGNAL SQLSTATE '45000'
        SET MESSAGE_TEXT = 'Нельзя удалить студента: у него есть оценки';
    END IF;
END */;;
DELIMITER ;
/*!50003 SET sql_mode              = @saved_sql_mode */ ;
/*!50003 SET character_set_client  = @saved_cs_client */ ;
/*!50003 SET character_set_results = @saved_cs_results */ ;
/*!50003 SET collation_connection  = @saved_col_connection */ ;

--
-- Table structure for table `teacher_specialities`
--

DROP TABLE IF EXISTS `teacher_specialities`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!50503 SET character_set_client = utf8mb4 */;
CREATE TABLE `teacher_specialities` (
  `teacher_id` int NOT NULL,
  `speciality_code` varchar(20) NOT NULL,
  PRIMARY KEY (`teacher_id`,`speciality_code`),
  KEY `fk_ts_speciality` (`speciality_code`),
  CONSTRAINT `fk_ts_speciality` FOREIGN KEY (`speciality_code`) REFERENCES `specialities` (`speciality_code`) ON DELETE CASCADE ON UPDATE CASCADE,
  CONSTRAINT `fk_ts_teacher` FOREIGN KEY (`teacher_id`) REFERENCES `teachers` (`teacher_id`) ON DELETE CASCADE ON UPDATE CASCADE
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_0900_ai_ci;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `teacher_specialities`
--

LOCK TABLES `teacher_specialities` WRITE;
/*!40000 ALTER TABLE `teacher_specialities` DISABLE KEYS */;
INSERT INTO `teacher_specialities` VALUES (1,'09.02.07'),(2,'09.02.07'),(7,'09.02.07'),(8,'09.02.07'),(5,'15.02.16'),(7,'15.02.16'),(8,'15.02.16'),(10,'15.02.16'),(4,'35.02.03'),(10,'35.02.03'),(3,'38.02.01'),(7,'38.02.01'),(9,'38.02.01'),(6,'40.02.01'),(9,'40.02.01'),(6,'40.02.04');
/*!40000 ALTER TABLE `teacher_specialities` ENABLE KEYS */;
UNLOCK TABLES;

--
-- Table structure for table `teachers`
--

DROP TABLE IF EXISTS `teachers`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!50503 SET character_set_client = utf8mb4 */;
CREATE TABLE `teachers` (
  `teacher_id` int NOT NULL AUTO_INCREMENT,
  `tab_number` varchar(20) NOT NULL,
  `surname` varchar(100) NOT NULL,
  `name` varchar(100) NOT NULL,
  `patronymic` varchar(100) DEFAULT NULL,
  `birth_date` date DEFAULT NULL,
  `academic_degree` varchar(100) DEFAULT NULL,
  `position` varchar(100) DEFAULT NULL,
  `hire_date` date DEFAULT NULL,
  `cabinet` varchar(20) DEFAULT NULL,
  `email` varchar(100) DEFAULT NULL,
  `phone` varchar(30) DEFAULT NULL,
  PRIMARY KEY (`teacher_id`),
  UNIQUE KEY `tab_number` (`tab_number`)
) ENGINE=InnoDB AUTO_INCREMENT=11 DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_0900_ai_ci;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `teachers`
--

LOCK TABLES `teachers` WRITE;
/*!40000 ALTER TABLE `teachers` DISABLE KEYS */;
INSERT INTO `teachers` VALUES (1,'ПРП-001','Петрова','Наталья','Владимировна','1985-05-12','б/с','преподаватель','2010-09-01','305','petrova@college.ru','+7-900-111-22-33'),(2,'ПРП-002','Сидоров','Андрей','Михайлович','1979-08-03','к.т.н.','ст. преподаватель','2008-09-01','307','sidorov@college.ru','+7-900-222-33-44'),(3,'ПРП-003','Кузьмина','Татьяна','Сергеевна','1982-11-28','б/с','преподаватель','2012-09-01','201','kuzmina@college.ru','+7-900-333-44-55'),(4,'ПРП-004','Волков','Евгений','Романович','1975-04-15','б/с','преподаватель','2005-09-01','102','volkov@college.ru','+7-900-444-55-66'),(5,'ПРП-005','Громов','Пётр','Иванович','1980-07-07','к.т.н.','ст. преподаватель','2009-09-01','115','gromov@college.ru','+7-900-555-66-77'),(6,'ПРП-006','Миронова','Ксения','Александровна','1983-02-20','к.ю.н.','ст. преподаватель','2011-09-01','210','mironova@college.ru','+7-900-666-77-88'),(7,'ПРП-007','Ермакова','Светлана','Ивановна','1978-01-11','б/с','преподаватель','2007-09-01','204','ermakova@college.ru','+7-900-777-88-99'),(8,'ПРП-008','Пучков','Юрий','Васильевич','1970-06-05','к.т.н.','директор','2000-09-01','101','director@college.ru','+7-900-888-99-00'),(9,'ПРП-009','Зверева','Марина','Олеговна','1986-09-30','б/с','преподаватель','2015-09-01','302','zvereva@college.ru','+7-900-121-23-34'),(10,'ПРП-010','Капустин','Игорь','Николаевич','1977-03-17','к.п.н.','зав. отделением','2004-09-01','103','kapustin@college.ru','+7-900-232-34-45');
/*!40000 ALTER TABLE `teachers` ENABLE KEYS */;
UNLOCK TABLES;

--
-- Table structure for table `users`
--

DROP TABLE IF EXISTS `users`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!50503 SET character_set_client = utf8mb4 */;
CREATE TABLE `users` (
  `id` int NOT NULL AUTO_INCREMENT,
  `login` varchar(50) NOT NULL,
  `password` varchar(100) NOT NULL,
  `role` enum('admin','manager','employee') NOT NULL,
  `surname` varchar(100) NOT NULL,
  `name` varchar(100) NOT NULL,
  `patronymic` varchar(100) NOT NULL,
  PRIMARY KEY (`id`),
  UNIQUE KEY `login` (`login`)
) ENGINE=InnoDB AUTO_INCREMENT=8 DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_0900_ai_ci;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `users`
--

LOCK TABLES `users` WRITE;
/*!40000 ALTER TABLE `users` DISABLE KEYS */;
INSERT INTO `users` VALUES (1,'admin1','admin1','admin','Иванов','Сергей','Петрович'),(2,'manager1','manager1','manager','Петрова','Анна','Викторовна'),(3,'manager2','manager2','manager','Смирнов','Олег','Андреевич'),(4,'employee1','employee1','employee','Кузнецова','Мария','Игоревна'),(5,'employee2','employee2','employee','Волков','Дмитрий','Сергеевич'),(6,'employee3','employee3','employee','Соколова','Елена','Николаевна'),(7,'employee4','employee4','employee','Федоров','Андрей','Михайлович');
/*!40000 ALTER TABLE `users` ENABLE KEYS */;
UNLOCK TABLES;

--
-- Dumping events for database 'college'
--

--
-- Dumping routines for database 'college'
--
/*!50003 DROP PROCEDURE IF EXISTS `sp_add_performance` */;
/*!50003 SET @saved_cs_client      = @@character_set_client */ ;
/*!50003 SET @saved_cs_results     = @@character_set_results */ ;
/*!50003 SET @saved_col_connection = @@collation_connection */ ;
/*!50003 SET character_set_client  = utf8mb4 */ ;
/*!50003 SET character_set_results = utf8mb4 */ ;
/*!50003 SET collation_connection  = utf8mb4_0900_ai_ci */ ;
/*!50003 SET @saved_sql_mode       = @@sql_mode */ ;
/*!50003 SET sql_mode              = 'ONLY_FULL_GROUP_BY,STRICT_TRANS_TABLES,NO_ZERO_IN_DATE,NO_ZERO_DATE,ERROR_FOR_DIVISION_BY_ZERO,NO_ENGINE_SUBSTITUTION' */ ;
DELIMITER ;;
CREATE DEFINER=`root`@`localhost` PROCEDURE `sp_add_performance`(
    IN p_record_book_no VARCHAR(20),
    IN p_discipline_id VARCHAR(20),
    IN p_semester INT,
    IN p_grade DECIMAL(3,1),
    IN p_attestation_date DATE,
    IN p_teacher_id INT
)
BEGIN
    IF (SELECT COUNT(*) FROM students WHERE record_book_no = p_record_book_no) = 0 THEN
        SIGNAL SQLSTATE '45000'
        SET MESSAGE_TEXT = 'Студент не найден';
    END IF;

    IF (SELECT COUNT(*) FROM disciplines WHERE discipline_id = p_discipline_id) = 0 THEN
        SIGNAL SQLSTATE '45000'
        SET MESSAGE_TEXT = 'Дисциплина не найдена';
    END IF;

    INSERT INTO performance (
        record_book_no, discipline_id, semester,
        grade, attestation_date, teacher_id
    )
    VALUES (
        p_record_book_no, p_discipline_id, p_semester,
        p_grade, p_attestation_date, p_teacher_id
    );
END ;;
DELIMITER ;
/*!50003 SET sql_mode              = @saved_sql_mode */ ;
/*!50003 SET character_set_client  = @saved_cs_client */ ;
/*!50003 SET character_set_results = @saved_cs_results */ ;
/*!50003 SET collation_connection  = @saved_col_connection */ ;
/*!50003 DROP PROCEDURE IF EXISTS `sp_add_student` */;
/*!50003 SET @saved_cs_client      = @@character_set_client */ ;
/*!50003 SET @saved_cs_results     = @@character_set_results */ ;
/*!50003 SET @saved_col_connection = @@collation_connection */ ;
/*!50003 SET character_set_client  = utf8mb4 */ ;
/*!50003 SET character_set_results = utf8mb4 */ ;
/*!50003 SET collation_connection  = utf8mb4_0900_ai_ci */ ;
/*!50003 SET @saved_sql_mode       = @@sql_mode */ ;
/*!50003 SET sql_mode              = 'ONLY_FULL_GROUP_BY,STRICT_TRANS_TABLES,NO_ZERO_IN_DATE,NO_ZERO_DATE,ERROR_FOR_DIVISION_BY_ZERO,NO_ENGINE_SUBSTITUTION' */ ;
DELIMITER ;;
CREATE DEFINER=`root`@`localhost` PROCEDURE `sp_add_student`(
    IN p_record_book_no VARCHAR(20),
    IN p_surname VARCHAR(100),
    IN p_name VARCHAR(100),
    IN p_patronymic VARCHAR(100),
    IN p_birth_date DATE,
    IN p_group_id INT,
    IN p_student_status VARCHAR(50),
    IN p_study_form VARCHAR(50)
)
BEGIN
    IF (SELECT COUNT(*) FROM student_groups WHERE group_id = p_group_id) = 0 THEN
        SIGNAL SQLSTATE '45000'
        SET MESSAGE_TEXT = 'Группа с таким group_id не найдена';
    END IF;

    INSERT INTO students (
        record_book_no, surname, name, patronymic,
        birth_date, group_id, student_status, study_form
    )
    VALUES (
        p_record_book_no, p_surname, p_name, p_patronymic,
        p_birth_date, p_group_id, p_student_status, p_study_form
    );
END ;;
DELIMITER ;
/*!50003 SET sql_mode              = @saved_sql_mode */ ;
/*!50003 SET character_set_client  = @saved_cs_client */ ;
/*!50003 SET character_set_results = @saved_cs_results */ ;
/*!50003 SET collation_connection  = @saved_col_connection */ ;
/*!50003 DROP PROCEDURE IF EXISTS `sp_get_students_by_group` */;
/*!50003 SET @saved_cs_client      = @@character_set_client */ ;
/*!50003 SET @saved_cs_results     = @@character_set_results */ ;
/*!50003 SET @saved_col_connection = @@collation_connection */ ;
/*!50003 SET character_set_client  = utf8mb4 */ ;
/*!50003 SET character_set_results = utf8mb4 */ ;
/*!50003 SET collation_connection  = utf8mb4_0900_ai_ci */ ;
/*!50003 SET @saved_sql_mode       = @@sql_mode */ ;
/*!50003 SET sql_mode              = 'ONLY_FULL_GROUP_BY,STRICT_TRANS_TABLES,NO_ZERO_IN_DATE,NO_ZERO_DATE,ERROR_FOR_DIVISION_BY_ZERO,NO_ENGINE_SUBSTITUTION' */ ;
DELIMITER ;;
CREATE DEFINER=`root`@`localhost` PROCEDURE `sp_get_students_by_group`(
    IN p_group_id INT
)
BEGIN
    SELECT
        s.record_book_no,
        CONCAT_WS(' ', s.surname, s.name, s.patronymic) AS student_fio,
        g.group_number,
        s.birth_date,
        s.student_status,
        s.study_form
    FROM students s
    JOIN student_groups g ON g.group_id = s.group_id
    WHERE g.group_id = p_group_id
    ORDER BY s.surname, s.name, s.patronymic;
END ;;
DELIMITER ;
/*!50003 SET sql_mode              = @saved_sql_mode */ ;
/*!50003 SET character_set_client  = @saved_cs_client */ ;
/*!50003 SET character_set_results = @saved_cs_results */ ;
/*!50003 SET collation_connection  = @saved_col_connection */ ;
/*!40103 SET TIME_ZONE=@OLD_TIME_ZONE */;

/*!40101 SET SQL_MODE=@OLD_SQL_MODE */;
/*!40014 SET FOREIGN_KEY_CHECKS=@OLD_FOREIGN_KEY_CHECKS */;
/*!40014 SET UNIQUE_CHECKS=@OLD_UNIQUE_CHECKS */;
/*!40101 SET CHARACTER_SET_CLIENT=@OLD_CHARACTER_SET_CLIENT */;
/*!40101 SET CHARACTER_SET_RESULTS=@OLD_CHARACTER_SET_RESULTS */;
/*!40101 SET COLLATION_CONNECTION=@OLD_COLLATION_CONNECTION */;
/*!40111 SET SQL_NOTES=@OLD_SQL_NOTES */;

-- Dump completed on 2026-06-23 20:21:34
