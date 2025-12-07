-- Create users schema for internal and external user data

CREATE SCHEMA IF NOT EXISTS users;

CREATE TABLE IF NOT EXISTS users.r_roles (
	id INT NOT NULL,
	role_name VARCHAR(255) NOT NULL
	role_description TEXT NOT NULL
);

ALTER TABLE users.r_roles ADD CONSTRAINT pkey_r_roles PRIMARY KEY (id);

INSERT INTO users.r_roles (id, role_name, role_description) 
VALUES (
	1,
	'wristband-admin',
	'General admin privileges'
);
INSERT INTO users.r_roles (id, role_name, role_description) 
VALUES (
	2,
	'artist-owner',
	'This person has ownership rights over an artist and all the albums and songs of the artist.'
);

CREATE TABLE IF NOT EXISTS users.user_global_roles (
	id uuid NOT NULL,
	user_id uuid NOT NULL,
	role_id INT NOT NULL,
	created_datetime TIMESTAMP WITH TIME ZONE NOT NULL,
	updated_datetime TIMESTAMP WITH TIME ZONE,
	removed_datetime TIMESTAMP WITH TIME ZONE,
	created_by uuid NOT NULL,
	updated_by uuid,
	removed_by uuid
);

ALTER TABLE users.user_global_roles ADD CONSTRAINT pkey_user_global_roles PRIMARY KEY (id);
ALTER TABLE users.user_global_roles ADD CONSTRAINT fkey_user_global_roles_role FOREIGN KEY (role_id) REFERENCES users.r_roles (id);

CREATE TABLE IF NOT EXISTS users.user_artist_roles (
	id uuid NOT NULL,
	user_id uuid NOT NULL,
	role_id INT NOT NULL,
	artist_id uuid NOT NULL,
	created_datetime TIMESTAMP WITH TIME ZONE NOT NULL,
	updated_datetime TIMESTAMP WITH TIME ZONE,
	removed_datetime TIMESTAMP WITH TIME ZONE,
	created_by uuid NOT NULL,
	updated_by uuid,
	removed_by uuid
);

ALTER TABLE users.user_artist_roles ADD CONSTRAINT user_artist_roles PRIMARY KEY (id);
ALTER TABLE users.user_artist_roles ADD CONSTRAINT fkey_user_artist_roles_role FOREIGN KEY (role_id) REFERENCES users.r_roles (id);
ALTER TABLE users.user_artist_roles ADD CONSTRAINT fkey_user_artist_roles_artist FOREIGN KEY (artist_id) REFERENCES music.artists (id);

CREATE TABLE IF NOT EXISTS users.users (
	id uuid NOT NULL,
	username VARCHAR(255),
	first_name VARCHAR(255),
	last_name VARCHAR(255),
	email VARCHAR(255) NOT NULL,
	created_datetime TIMESTAMP WITH TIME ZONE NOT NULL,
	updated_datetime TIMESTAMP WITH TIME ZONE,
	removed_datetime TIMESTAMP WITH TIME ZONE,
	created_by uuid NOT NULL,
	updated_by uuid,
	removed_by uuid,
	paging_order INT GENERATED ALWAYS AS IDENTITY
);

ALTER TABLE users.users ADD CONSTRAINT pkey_users PRIMARY KEY (id);

-- Create music schema for music-related data

CREATE SCHEMA IF NOT EXISTS music;

CREATE TABLE IF NOT EXISTS music.artists (
	id uuid NOT NULL,
	artist_name VARCHAR(255) NOT NULL,
	hometown_zipcode VARCHAR(10) NOT NULL,
	current_zipcode VARCHAR(10) NOT NULL,
	created_datetime TIMESTAMP WITH TIME ZONE NOT NULL,
	updated_datetime TIMESTAMP WITH TIME ZONE,
	removed_datetime TIMESTAMP WITH TIME ZONE,
	created_by uuid NOT NULL,
	updated_by uuid,
	removed_by uuid,
	paging_order INT GENERATED ALWAYS AS IDENTITY
);

ALTER TABLE music.artists ADD CONSTRAINT pkey_artists PRIMARY KEY (id);

CREATE TABLE IF NOT EXISTS music.albums (
	id uuid NOT NULL,
	album_name VARCHAR(255) NOT NULL,
	artist_id uuid NOT NULL,
	album_purchase_url TEXT,
	album_art_url TEXT NOT NULL,
	created_datetime TIMESTAMP WITH TIME ZONE NOT NULL,
	updated_datetime TIMESTAMP WITH TIME ZONE,
	removed_datetime TIMESTAMP WITH TIME ZONE,
	created_by uuid NOT NULL,
	updated_by uuid,
	removed_by uuid,
	paging_order INT GENERATED ALWAYS AS IDENTITY
);

ALTER TABLE music.albums ADD CONSTRAINT pkey_albums PRIMARY KEY (id);
ALTER TABLE music.albums ADD CONSTRAINT fkey_albums_artist FOREIGN KEY (artist_id) REFERENCES music.artists (id);

CREATE TABLE IF NOT EXISTS music.r_genres (
	id INT NOT NULL,
	genre_name VARCHAR(255) NOT NULL
);

ALTER TABLE music.r_genres ADD CONSTRAINT pkey_r_genres PRIMARY KEY (id);


INSERT INTO music.r_genres (id, genre_name) VALUES (0, 'metal');
INSERT INTO music.r_genres (id, genre_name) VALUES (1, 'alternative');
INSERT INTO music.r_genres (id, genre_name) VALUES (2, 'blues');
INSERT INTO music.r_genres (id, genre_name) VALUES (3, 'classical');
INSERT INTO music.r_genres (id, genre_name) VALUES (4, 'country');
INSERT INTO music.r_genres (id, genre_name) VALUES (5, 'dance');
INSERT INTO music.r_genres (id, genre_name) VALUES (6, 'electronic');
INSERT INTO music.r_genres (id, genre_name) VALUES (7, 'folk');
INSERT INTO music.r_genres (id, genre_name) VALUES (8, 'hiphop');
INSERT INTO music.r_genres (id, genre_name) VALUES (9, 'jazz');
INSERT INTO music.r_genres (id, genre_name) VALUES (10, 'pop');

CREATE TABLE IF NOT EXISTS music.songs (
	id uuid NOT NULL,
	song_name VARCHAR(255) NOT NULL,
	artist_id uuid NOT NULL,
	album_id uuid NOT NULL,
	audio_url TEXT NOT NULL,
	track_purchase_url TEXT,
	album_order INT,
	genre_1 INT NOT NULL,
	genre_2 INT,
	genre_3 INT,
	genre_4 INT,
	genre_5 INT,
	is_explicit BOOLEAN,
	duration_in_seconds INT,
	status VARCHAR(24),
	created_datetime TIMESTAMP WITH TIME ZONE NOT NULL,
	updated_datetime TIMESTAMP WITH TIME ZONE,
	removed_datetime TIMESTAMP WITH TIME ZONE,
	created_by uuid NOT NULL,
	updated_by uuid,
	removed_by uuid,
	paging_order INT GENERATED ALWAYS AS IDENTITY,
);

ALTER TABLE music.songs ADD CONSTRAINT pkey_songs PRIMARY KEY (id);
ALTER TABLE music.songs ADD CONSTRAINT fkey_songs_artist FOREIGN KEY (artist_id) REFERENCES music.artists (id);
ALTER TABLE music.songs ADD CONSTRAINT fkey_songs_album FOREIGN KEY (album_id) REFERENCES music.albums (id);
ALTER TABLE music.songs ADD CONSTRAINT fkey_songs_genre_1 FOREIGN KEY (genre_1) REFERENCES music.r_genres (id);
ALTER TABLE music.songs ADD CONSTRAINT fkey_songs_genre_2 FOREIGN KEY (genre_2) REFERENCES music.r_genres (id);
ALTER TABLE music.songs ADD CONSTRAINT fkey_songs_genre_3 FOREIGN KEY (genre_3) REFERENCES music.r_genres (id);
ALTER TABLE music.songs ADD CONSTRAINT fkey_songs_genre_4 FOREIGN KEY (genre_4) REFERENCES music.r_genres (id);
ALTER TABLE music.songs ADD CONSTRAINT fkey_songs_genre_5 FOREIGN KEY (genre_5) REFERENCES music.r_genres (id);

CREATE TABLE IF NOT EXISTS music.spins(
	id uuid NOT NULL,
	song_id uuid NOT NULL,
	user_id uuid NOT NULL,
	start_datetime TIMESTAMP WITH TIME ZONE NOT NULL,
	end_datetime TIMESTAMP WITH TIME ZONE,
	created_datetime TIMESTAMP WITH TIME ZONE NOT NULL,
	updated_datetime TIMESTAMP WITH TIME ZONE,
	removed_datetime TIMESTAMP WITH TIME ZONE,
	created_by uuid NOT NULL,
	updated_by uuid,
	removed_by uuid
);

ALTER TABLE music.spins ADD CONSTRAINT pkey_spins PRIMARY KEY (id);
ALTER TABLE music.spins ADD CONSTRAINT fkey_spins_song FOREIGN KEY (song_id) REFERENCES music.songs (id);

CREATE VIEW music.tracks AS
SELECT
    s.id AS song_id,
    s.song_name,
    s.audio_url,
    s.track_purchase_url,
	s.duration_in_seconds,
    art.artist_name,
    ab.album_name,
    ab.album_art_url,
    s.genre_1,
    s.genre_2,
    s.genre_3,
    s.genre_4,
    s.genre_5,
    s.paging_order,
	s.is_explicit,
	s.status,
	s.removed_datetime
FROM music.songs s
LEFT JOIN music.artists art ON s.artist_id = art.id
LEFT JOIN music.albums ab ON s.album_id = ab.id;

CREATE VIEW music.user_owned_tracks AS
SELECT
	uar.user_id,
	s.id AS song_id,
    s.song_name,
    s.audio_url,
    s.track_purchase_url,
	s.duration_in_seconds,
    art.artist_name,
    ab.album_name,
    ab.album_art_url,
    s.genre_1,
    s.genre_2,
    s.genre_3,
    s.genre_4,
    s.genre_5,
    s.paging_order,
	s.is_explicit,
	s.status,
	s.removed_datetime
FROM users.user_artist_roles uar
LEFT JOIN music.artists art ON uar.artist_id = art.id
LEFT JOIN music.albums ab ON uar.artist_id = ab.artist_id
LEFT JOIN music.songs s ON uar.artist_id = s.artist_id
WHERE uar.role_id=2;

CREATE VIEW music.artist_albums AS
SELECT
	ar.id as artist_id
	ar.artist_name,
	ar.hometown_zipcode,
	ar.current_zipcode,
	al.id as album_id,
	al.album_name,
	al.album_art_url,
	al.album_purchase_url
FROM music.artists ar
LEFT JOIN music.albums al ON ar.id = al.artist_id;

-- Create submissions schema for data submitted by users
CREATE SCHEMA IF NOT EXISTS submissions;

CREATE TABLE IF NOT EXISTS submissions.r_submission_types (
	id INT AS IDENTITY,
	submission_type_name VARCHAR(255) NOT NULL
);

ALTER TABLE submissions.submission_types ADD CONSTRAINT pkey_submission_types PRIMARY KEY (id);

CREATE TABLE IF NOT EXISTS submissions.music_submissions (
	id uuid NOT NULL,
	submission_type_id uuid NOT NULL,
	user_id uuid,
	artist_name VARCHAR(255) NOT NULL,
	contact_name VARCHAR(255) NOT NULL,
	contact_email VARCHAR(255) NOT NULL,
	contact_phone VARCHAR(20) NOT NULL,
    status VARCHAR(20) NOT NULL,
    owns_rights BOOLEAN,
	created_datetime TIMESTAMP WITH TIME ZONE NOT NULL,
	updated_datetime TIMESTAMP WITH TIME ZONE,
	removed_datetime TIMESTAMP WITH TIME ZONE,
	created_by uuid NOT NULL,
	updated_by uuid,
	removed_by uuid,
    paging_order INT GENERATED ALWAYS AS IDENTITY
);

ALTER TABLE submissions.music_submissions ADD CONSTRAINT pkey_music_submissions PRIMARY KEY (id);

CREATE TABLE IF NOT EXISTS submissions.music_submission_audio_links (
	id uuid NOT NULL,
	music_submission_id uuid NOT NULL,
	audio_url TEXT NOT NULL,
	created_datetime TIMESTAMP WITH TIME ZONE NOT NULL,
	updated_datetime TIMESTAMP WITH TIME ZONE,
	removed_datetime TIMESTAMP WITH TIME ZONE,
	created_by uuid NOT NULL,
	updated_by uuid,
	removed_by uuid
);

ALTER TABLE submissions.music_submission_audio_links ADD CONSTRAINT pkey_music_submission_audio_links PRIMARY KEY (id);
ALTER TABLE submissions.music_submission_audio_links ADD CONSTRAINT fkey_music_submission_audio_links_submission FOREIGN KEY (music_submission_id) REFERENCES submissions.music_submissions (id);

CREATE TABLE IF NOT EXISTS submissions.music_submission_image_links (
	id uuid NOT NULL,
	music_submission_id uuid NOT NULL,
	image_url TEXT NOT NULL,
	created_datetime TIMESTAMP WITH TIME ZONE NOT NULL,
	updated_datetime TIMESTAMP WITH TIME ZONE,
	removed_datetime TIMESTAMP WITH TIME ZONE,
	created_by uuid NOT NULL,
	updated_by uuid,
	removed_by uuid
);

ALTER TABLE submissions.music_submission_image_links ADD CONSTRAINT pkey_music_submission_image_links PRIMARY KEY (id);
ALTER TABLE submissions.music_submission_image_links ADD CONSTRAINT fkey_music_submission_image_links_submission FOREIGN KEY (music_submission_id) REFERENCES submissions.music_submissions (id);

CREATE TABLE IF NOT EXISTS submissions.feedback_submissions (
	id uuid NOT NULL,
	contact_name VARCHAR(255) NOT NULL,
	contact_email VARCHAR(255) NOT NULL,
	contact_phone VARCHAR(20) NOT NULL,
	submission_type_id INT NOT NULL,
	feedback_text TEXT NOT NULL,
	created_datetime TIMESTAMP WITH TIME ZONE NOT NULL,
	updated_datetime TIMESTAMP WITH TIME ZONE,
	removed_datetime TIMESTAMP WITH TIME ZONE,
	created_by uuid NOT NULL,
	updated_by uuid,
	removed_by uuid,
	status VARCHAR(50) NOT NULL,
	paging_order INT GENERATED ALWAYS AS IDENTITY
);

ALTER TABLE submissions.feedback_submissions ADD CONSTRAINT pkey_feedback_submissions PRIMARY KEY (id);
ALTER TABLE submissions.feedback_submissions ADD CONSTRAINT fkey_feedback_submissions_submission_type FOREIGN KEY (submission_type_id) REFERENCES submissions.submission_types (id);
