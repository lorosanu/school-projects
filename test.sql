-- phpMyAdmin SQL Dump
-- version 4.5.2
-- http://www.phpmyadmin.net
--
-- Host: 127.0.0.1
-- Generation Time: Jun 21, 2016 at 10:24 AM
-- Server version: 5.7.9
-- PHP Version: 5.6.16

SET SQL_MODE = "NO_AUTO_VALUE_ON_ZERO";
SET time_zone = "+00:00";


/*!40101 SET @OLD_CHARACTER_SET_CLIENT=@@CHARACTER_SET_CLIENT */;
/*!40101 SET @OLD_CHARACTER_SET_RESULTS=@@CHARACTER_SET_RESULTS */;
/*!40101 SET @OLD_COLLATION_CONNECTION=@@COLLATION_CONNECTION */;
/*!40101 SET NAMES utf8mb4 */;

--
-- Database: `test`
--

-- --------------------------------------------------------

--
-- Table structure for table `minichat`
--

DROP TABLE IF EXISTS `minichat`;
CREATE TABLE IF NOT EXISTS `minichat` (
  `id` int(11) NOT NULL AUTO_INCREMENT,
  `pseudo` varchar(255) NOT NULL,
  `message` varchar(255) NOT NULL,
  `date_creation` datetime NOT NULL,
  PRIMARY KEY (`id`)
) ENGINE=MyISAM AUTO_INCREMENT=25 DEFAULT CHARSET=latin1;

--
-- Dumping data for table `minichat`
--

INSERT INTO `minichat` (`id`, `pseudo`, `message`, `date_creation`) VALUES
(1, 'Julien', 'Bonjour !', '2016-06-21 10:45:45'),
(2, 'Julien', 'y a quelqu''un ?', '2016-06-21 10:47:24'),
(3, 'Julien', 'Allô allô ?', '2016-06-21 10:48:33'),
(4, 'Alex', 'Bonjour Julien !', '2016-06-21 10:48:48'),
(5, 'Emilie', 'Coucou les garçons !', '2016-06-21 10:49:32'),
(6, 'Emilie', 'Vous allez bien ?', '2016-06-21 10:50:01'),
(7, 'Alex', 'Coucou Emilie !', '2016-06-21 10:54:01'),
(8, 'Alex', 'Ça va bien, et toi ?', '2016-06-21 10:56:14'),
(9, 'Emilie', 'Super, c''est les vacances !', '2016-06-21 10:57:40'),
(10, 'Julien', 'Alala ... la chance ...', '2016-06-21 10:58:09'),
(11, 'Julien', 'Tu pars où ?', '2016-06-21 10:59:26'),
(12, 'Julien', 'à la mer ? à la montagne ? à la campagne ?', '2016-06-21 11:08:33'),
(13, 'Alex', 'Moi aussi je suis en vacances ! ', '2016-06-21 11:13:27'),
(14, 'Julien', 'Est-ce que je suis le seul encore au travail ? ... :(', '2016-06-21 11:37:15'),
(15, 'Emilie', 'J''ai bien peut que oui ... :(', '2016-06-21 11:47:01'),
(16, 'Emilie', 'Thomas est aussi parti en vacances', '2016-06-21 11:47:22'),
(17, 'Alex', 'Tony aussi ...', '2016-06-21 11:47:36'),
(18, 'Emilie', 'Sinon, moi je pars à la montagne avec ma famille :)', '2016-06-21 11:52:23'),
(19, 'Emilie', 'pour deux semaines', '2016-06-21 11:52:36'),
(20, 'Julien', 'pas mal ! ', '2016-06-21 11:53:38'),
(21, 'Alex', 'et moi je vais en Espagne avec ma femme !', '2016-06-21 11:54:13'),
(22, 'Julien', 'encore mieux !', '2016-06-21 11:54:22'),
(23, 'Julien', 'moi je vais aller manger', '2016-06-21 12:23:02'),
(24, 'Julien', 'a+', '2016-06-21 12:23:26');

/*!40101 SET CHARACTER_SET_CLIENT=@OLD_CHARACTER_SET_CLIENT */;
/*!40101 SET CHARACTER_SET_RESULTS=@OLD_CHARACTER_SET_RESULTS */;
/*!40101 SET COLLATION_CONNECTION=@OLD_COLLATION_CONNECTION */;
