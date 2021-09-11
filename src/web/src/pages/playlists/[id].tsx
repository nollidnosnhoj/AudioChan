import { GetServerSideProps } from "next";
import React from "react";
import Page from "~/components/Page";
import AudioList from "~/features/audio/components/List";
import { useGetPlaylist } from "~/features/playlist/api/hooks";
import { useGetPlaylistAudios } from "~/features/playlist/api/hooks/useGetPlaylistAudios";
import { Playlist } from "~/features/playlist/api/types";
import PlaylistDetails from "~/features/playlist/components/Details";
import request from "~/lib/http";

interface PlaylistPageProps {
  playlist: Playlist;
  playlistId: number;
}

export const getServerSideProps: GetServerSideProps<PlaylistPageProps> = async (
  context
) => {
  const { req, res, params } = context;
  try {
    const id = parseInt(params?.id as string, 10);

    if (isNaN(id)) {
      return {
        notFound: true,
      };
    }

    const { data } = await request<Playlist>({
      method: "GET",
      url: `playlists/${id}`,
      req,
      res,
    });

    return {
      props: {
        playlist: data,
        playlistId: id,
      },
    };
  } catch (err) {
    return {
      notFound: true,
    };
  }
};

export default function PlaylistPage({
  playlist: initPlaylist,
  playlistId,
}: PlaylistPageProps) {
  const { data: playlist } = useGetPlaylist(playlistId, {
    enabled: !!playlistId,
    initialData: initPlaylist,
  });
  const { items: playlistAudios } = useGetPlaylistAudios(playlist?.id);

  if (!playlist) return null;

  return (
    <Page title="Playlist">
      <PlaylistDetails playlist={playlist} />
      <AudioList audios={playlistAudios} context={`playlist:${playlist.id}`} />
    </Page>
  );
}
