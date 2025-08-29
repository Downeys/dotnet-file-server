-- Create users schema for internal and external user data

CREATE SCHEMA IF NOT EXISTS users;

CREATE TABLE IF NOT EXISTS users.r_user_types (
	id uuid NOT NULL,
	user_type_name VARCHAR(255) NOT NULL
);

ALTER TABLE users.r_user_types ADD CONSTRAINT pkey_r_user_types PRIMARY KEY (id);

CREATE TABLE IF NOT EXISTS users.user_types (
	id uuid NOT NULL,
	user_id uuid NOT NULL,
	user_type_id uuid NOT NULL,
	created_datetime TIMESTAMP WITH TIME ZONE NOT NULL,
	updated_datetime TIMESTAMP WITH TIME ZONE NOT NULL,
	removed_datetime TIMESTAMP WITH TIME ZONE,
	created_by uuid NOT NULL,
	updated_by uuid NOT NULL,
	removed_by uuid
);

ALTER TABLE users.user_types ADD CONSTRAINT pkey_user_types PRIMARY KEY (id);
ALTER TABLE users.user_types ADD CONSTRAINT fkey_user_types_user_type FOREIGN KEY (user_type_id) REFERENCES users.r_user_types (id);

CREATE TABLE IF NOT EXISTS users.r_roles (
	id uuid NOT NULL,
	role_name VARCHAR(255) NOT NULL
);

ALTER TABLE users.r_roles ADD CONSTRAINT pkey_r_roles PRIMARY KEY (id);

CREATE TABLE IF NOT EXISTS users.user_roles (
	id uuid NOT NULL,
	user_id uuid NOT NULL,
	role_id uuid NOT NULL,
	created_datetime TIMESTAMP WITH TIME ZONE NOT NULL,
	updated_datetime TIMESTAMP WITH TIME ZONE NOT NULL,
	removed_datetime TIMESTAMP WITH TIME ZONE,
	created_by uuid NOT NULL,
	updated_by uuid NOT NULL,
	removed_by uuid
);

ALTER TABLE users.user_roles ADD CONSTRAINT pkey_user_roles PRIMARY KEY (id);
ALTER TABLE users.user_roles ADD CONSTRAINT fkey_user_roles_role FOREIGN KEY (role_id) REFERENCES users.r_roles (id);

CREATE TABLE IF NOT EXISTS users.artist_user_roles (
	id uuid NOT NULL,
	user_id uuid NOT NULL,
	role_id uuid NOT NULL,
	artist_id uuid NOT NULL,
	created_datetime TIMESTAMP WITH TIME ZONE NOT NULL,
	updated_datetime TIMESTAMP WITH TIME ZONE NOT NULL,
	removed_datetime TIMESTAMP WITH TIME ZONE,
	created_by uuid NOT NULL,
	updated_by uuid NOT NULL,
	removed_by uuid
);

ALTER TABLE users.artist_user_roles ADD CONSTRAINT pkey_artist_user_roles PRIMARY KEY (id);
ALTER TABLE users.artist_user_roles ADD CONSTRAINT fkey_artist_user_roles_role FOREIGN KEY (role_id) REFERENCES users.r_roles (id);
ALTER TABLE users.artist_user_roles ADD CONSTRAINT fkey_artist_user_roles_artist FOREIGN KEY (artist_id) REFERENCES music.artists (id);

CREATE TABLE IF NOT EXISTS users.internal_users (
	id uuid NOT NULL,
	first_name VARCHAR(255) NOT NULL,
	last_name VARCHAR(255) NOT NULL,
	email VARCHAR(255) NOT NULL,
	created_datetime TIMESTAMP WITH TIME ZONE NOT NULL,
	updated_datetime TIMESTAMP WITH TIME ZONE NOT NULL,
	removed_datetime TIMESTAMP WITH TIME ZONE,
	created_by uuid NOT NULL,
	updated_by uuid NOT NULL,
	removed_by uuid
);

ALTER TABLE users.internal_users ADD CONSTRAINT pkey_internal_users PRIMARY KEY (id);

CREATE TABLE IF NOT EXISTS users.external_users (
	id uuid NOT NULL,
	username VARCHAR(255),
	first_name VARCHAR(255),
	last_name VARCHAR(255),
	email VARCHAR(255) NOT NULL,
	created_datetime TIMESTAMP WITH TIME ZONE NOT NULL,
	updated_datetime TIMESTAMP WITH TIME ZONE NOT NULL,
	removed_datetime TIMESTAMP WITH TIME ZONE,
	created_by uuid NOT NULL,
	updated_by uuid NOT NULL,
	removed_by uuid
);

ALTER TABLE users.external_users ADD CONSTRAINT pkey_external_users PRIMARY KEY (id);

-- Create music schema for music-related data

CREATE SCHEMA IF NOT EXISTS music;

CREATE TABLE IF NOT EXISTS music.artists (
	id uuid NOT NULL,
	artist_name VARCHAR(255) NOT NULL,
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

CREATE TABLE IF NOT EXISTS music.songs (
	id uuid NOT NULL,
	song_name VARCHAR(255) NOT NULL,
	artist_id uuid NOT NULL,
	album_id uuid NOT NULL,
	audio_url TEXT NOT NULL,
	track_purchase_url TEXT,
	genre_1 INT NOT NULL,
	genre_2 INT,
	genre_3 INT,
	genre_4 INT,
	genre_5 INT,
	created_datetime TIMESTAMP WITH TIME ZONE NOT NULL,
	updated_datetime TIMESTAMP WITH TIME ZONE,
	removed_datetime TIMESTAMP WITH TIME ZONE,
	created_by uuid NOT NULL,
	updated_by uuid,
	removed_by uuid,
	paging_order INT GENERATED ALWAYS AS IDENTITY,
	is_explicit BOOLEAN
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
    s.id,
    s.song_name,
    s.audio_url,
    s.track_purchase_url,
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
	s.removed_datetime
FROM music.songs s
LEFT JOIN music.artists art ON s.artist_id = art.id
LEFT JOIN music.albums ab ON s.album_id = ab.id;

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
