import { useMutation, UseMutationResult, useQueryClient } from "react-query";
import { AudioView } from "../types";
import { GET_AUDIO_QUERY_KEY } from "./useGetAudio";
import { GET_AUDIO_LIST_QUERY_KEY } from "./useGetAudioList";
import { useUser } from "~/features/user/hooks";
import { GET_USER_AUDIOS_QUERY_KEY } from "~/features/user/api/hooks/useGetUserAudios";
import { ErrorResponse, ID, ImageUploadResponse } from "~/lib/types";
import request from "~/lib/http";

export function useAddAudioPicture(
  audioId: ID
): UseMutationResult<ImageUploadResponse, ErrorResponse, string> {
  const queryClient = useQueryClient();
  const { user } = useUser();

  const uploadArtwork = async (data: string): Promise<ImageUploadResponse> => {
    const { data: result } = await request<ImageUploadResponse>({
      url: `audios/${audioId}/picture`,
      method: "PATCH",
      data: {
        data,
      },
    });
    return result;
  };

  return useMutation(uploadArtwork, {
    onSuccess(data) {
      const audio = queryClient.getQueryData<AudioView>(
        GET_AUDIO_QUERY_KEY(audioId)
      );

      if (audio) {
        queryClient.setQueryData<AudioView>(GET_AUDIO_QUERY_KEY(audioId), {
          ...audio,
          picture: data.url,
        });
      }

      queryClient.invalidateQueries(GET_AUDIO_LIST_QUERY_KEY);

      if (user) {
        queryClient.invalidateQueries(GET_USER_AUDIOS_QUERY_KEY(user.userName));
      }
    },
  });
}
